using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private Button changeButton;
    [SerializeField] private string sceneName;
    private void OnEnable()
    {
        changeButton = GetComponent<Button>();
        changeButton.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        StartCoroutine(SceneChangeManager.Instance.LoadScene(sceneName,0));
    }
}
