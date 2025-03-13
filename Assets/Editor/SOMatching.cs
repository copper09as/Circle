using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SOMatching : EditorWindow
{
    [MenuItem("SO/表格化检视工具")]
    private static void ShowWindow()
    {
        var win = GetWindow<SOMatching>("表格化检视");
        win.minSize = new Vector2(800, 400);
        win.Show();
    }

    private List<TScriptableObject> _allObjects = new List<TScriptableObject>();
    private List<bool> _selections = new List<bool>();
    private Vector2 _listScroll;
    private Vector2 _tableScroll;
    private List<Editor> _editors = new List<Editor>();
    private List<string> _propertyNames = new List<string>();
    private string _typeFilter = "";
    [SerializeField] private string _newInstancePath = "Assets/"; // 默认保存路径

    void OnEnable() => RefreshList();

    void RefreshList()
    {
        // 清理旧数据
        _allObjects.Clear();
        _selections.Clear();
        ClearEditors();

        // 查找所有TScriptableObject实例
        var guids = AssetDatabase.FindAssets("t:TScriptableObject");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var so = AssetDatabase.LoadAssetAtPath<TScriptableObject>(path);
            if (so != null)
            {
                // 应用类型过滤
                bool shouldAdd = true;
                if (!string.IsNullOrEmpty(_typeFilter))
                {
                    Type filterType = GetFilterType(_typeFilter);
                    Type objType = so.GetType();

                    shouldAdd = filterType != null ?
                        filterType.IsAssignableFrom(objType) :
                        objType.Name.IndexOf(_typeFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                }

                if (shouldAdd)
                {
                    _allObjects.Add(so);
                    _selections.Add(false);
                }
            }
        }

        // 初始化属性列表
        if (_allObjects.Count > 0)
            CachePropertyNames(_allObjects[0]);
    }
    Type GetFilterType(string typeName)
    {
        // 在程序集中查找匹配的类型
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t =>
                t.IsSubclassOf(typeof(TScriptableObject)) &&
                !t.IsAbstract &&
                t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
    }
    void CachePropertyNames(ScriptableObject sample)
    {
        _propertyNames.Clear();
        var so = new SerializedObject(sample);
        var prop = so.GetIterator();
        prop.NextVisible(true); // 跳过基类属性

        while (prop.NextVisible(false))
        {
            _propertyNames.Add(prop.name);
        }
    }

    void ClearEditors()
    {
        foreach (var editor in _editors)
            DestroyImmediate(editor);
        _editors.Clear();
    }

    void OnGUI()
    {
        DrawToolbar();
        using (new EditorGUILayout.HorizontalScope())
        {
            DrawObjectList();
            DrawPropertyTable();
        }
    }

    void DrawToolbar()
    {
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            // 类型过滤
            EditorGUI.BeginChangeCheck();
            _typeFilter = EditorGUILayout.TextField("类型过滤:", _typeFilter, GUILayout.Width(200));
            if (EditorGUI.EndChangeCheck())
                RefreshList();

            // 控制按钮
            if (GUILayout.Button("刷新列表", EditorStyles.toolbarButton))
                RefreshList();

            if (GUILayout.Button("全选", EditorStyles.toolbarButton))
                SelectAll(true);

            if (GUILayout.Button("清空", EditorStyles.toolbarButton))
                SelectAll(false);
        }
    }

    void SelectAll(bool state)
    {
        for (int i = 0; i < _selections.Count; i++)
            _selections[i] = state;
    }

    void DrawObjectList()
    {
        using (new EditorGUILayout.VerticalScope(GUILayout.Width(250)))
        {
            EditorGUILayout.LabelField("可选对象", EditorStyles.boldLabel);

            using (var scroll = new EditorGUILayout.ScrollViewScope(_listScroll))
            {
                _listScroll = scroll.scrollPosition;

                for (int i = 0; i < _allObjects.Count; i++)
                {
                    var so = _allObjects[i];
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        _selections[i] = EditorGUILayout.Toggle(_selections[i], GUILayout.Width(20));

                        var style = new GUIStyle(GUI.skin.button)
                        {
                            alignment = TextAnchor.MiddleLeft,
                            fixedHeight = 22
                        };

                        if (GUILayout.Button($"{so.name} ({so.GetType().Name})", style))
                            _selections[i] = !_selections[i];
                    }
                }
            }
        }
    }

    void DrawPropertyTable()
    {
        using (new EditorGUILayout.VerticalScope(GUILayout.ExpandWidth(true)))
        {
            EditorGUILayout.LabelField("属性表格", EditorStyles.boldLabel);

            using (var scroll = new EditorGUILayout.ScrollViewScope(_tableScroll))
            {
                _tableScroll = scroll.scrollPosition;

                var selectedObjects = GetSelectedObjects();
                if (selectedObjects.Count == 0)
                {
                    EditorGUILayout.HelpBox("请从左侧选择对象", MessageType.Info);
                    return;
                }

                // 创建编辑器实例
                if (_editors.Count != selectedObjects.Count)
                {
                    ClearEditors();
                    foreach (var obj in selectedObjects)
                        _editors.Add(Editor.CreateEditor(obj));
                }

                // 绘制表格
                DrawTableHeader();
                DrawTableRows();
            }
        }
    }

    void DrawTableHeader()
    {
        using (new EditorGUILayout.HorizontalScope(GUI.skin.box))
        {
            GUILayout.Label("对象名称", GUILayout.Width(120));
            foreach (var propName in _propertyNames)
            {
                var prop = new SerializedObject(_editors[0].target).FindProperty(propName);
                GUILayout.Label(prop != null ? prop.displayName : propName, GUILayout.Width(150));
            }
        }
    }

    void DrawTableRows()
    {
        foreach (var editor in _editors)
        {
            using (new EditorGUILayout.HorizontalScope(GUI.skin.box))
            {
                // 对象名称列
                EditorGUILayout.LabelField(editor.target.name, GUILayout.Width(120));

                // 属性列
                var so = new SerializedObject(editor.target);
                foreach (var propName in _propertyNames)
                {
                    using (new EditorGUILayout.VerticalScope(GUILayout.Width(150)))
                    {
                        var prop = so.FindProperty(propName);
                        if (prop != null)
                        {
                            EditorGUILayout.PropertyField(prop, GUIContent.none, GUILayout.Width(140));
                            so.ApplyModifiedProperties();
                        }
                        else
                        {
                            EditorGUILayout.LabelField("—", GUILayout.Width(20));
                        }
                    }
                }
            }
        }
    }

    List<TScriptableObject> GetSelectedObjects()
    {
        var list = new List<TScriptableObject>();
        for (int i = 0; i < _selections.Count; i++)
            if (_selections[i]) list.Add(_allObjects[i]);
        return list;
    }

    void OnDestroy() => ClearEditors();
}