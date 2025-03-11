using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;
using Npc.State;

public class Line : MonoBehaviour
{
    public Vector3 headPos;
    Vector3 endPos;

    Vector3 Pos;//当前向量
    Vector3 StartPos;//初始向量

    public double angle;//角度
    double rad;//向量的弧度值
    [Header("相对角速度")]
    public float s_speed;//设定相对角速度 rad/s
    public float speed;//实际角速度（相对坐标系）
    public double length;//长度
    [Header("上一条线")]
    public Line Last;
    public List<Line> Nexts;

    [Header("线渲染器和轨迹渲染器")]
    public LineRenderer lineRenderer;
    //Material lineMaterial;
    public LineRenderer trailRenderer;//轨迹渲染器

    private List<Vector3> trailPositions; // 轨迹点列表
    public int maxTrailPoints = 1000;//轨迹点上限
    void Awake()
    {
        Nexts = new List<Line>();//初始化
        rad = angle / 180 * Math.PI;//计算弧度值
        SetStartPos();
    }
    void Start()
    {
        Intialize();
    }
    void Intialize()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineMaterial = lineRenderer.material;
        if (lineRenderer != null)
        {
            trailRenderer.useWorldSpace = false;//使用局部坐标
            trailPositions = new List<Vector3>();
        }
        //初始长度初始向量计算与实际速度计算
        Pos = StartPos;//赋值当前向量
        //endPos = headPos + StartPos;//计算初始点坐标
        HideLine();//隐藏向量
    }

    private void FixedUpdate()
    {
        if(gameObject.CompareTag("HeadLine"))return;
        Turning();//旋转
        UpdateTrail();//轨迹描绘
    }



    //先序遍历森林
    public void Traversal(string H,string E)
    {
        //对每个结点执行一些操作
        //action1?.Invoke();
        if (H == "Refresh") Refresh();
        SetSpeed();
        SetHeadPos(SetEndPos());
        //此后改为switch
        foreach (Line line in Nexts)
        {
            line.Traversal(H, E);
        }//执行到空时
        //对每个尾节点结点执行一些操作
        //action2?.Invoke();
    }

    //创建森林
    public void Refresh()
        //获取父物体的Line
        //遍历每个一级子物体，并获取他们的Line组件到Nexts中
    {
        if(!gameObject.CompareTag("HeadLine")) 
            Last = transform.parent?.GetComponent<Line>();
        Nexts.Clear();//刷新
        foreach (Transform child in transform) // 遍历所有一级子物体
        {
            if (child.TryGetComponent<Line>(out var line))
            {
                Nexts.Add(line);
            }
        }
    }



    public void Turning()
    {

        if (Last != null)//给HeadPos赋值
            headPos = Last.endPos;

        lineRenderer.SetPosition(0, headPos);
        endPos = Calculation();
        lineRenderer.SetPosition(1, endPos);
    }
    private void UpdateTrail()
    {
        // 添加新点并限制数量
        trailPositions.Add(endPos);
        if (trailPositions.Count > maxTrailPoints)
        {
            trailPositions.RemoveAt(0);
        }

        // 更新轨迹LineRenderer
        trailRenderer.positionCount = trailPositions.Count;
        trailRenderer.SetPositions(trailPositions.ToArray());
    }
    public Vector3 Calculation()
    {
        double tanValue = Math.Atan2(Pos.y, Pos.x);//计算现在的弧度值
        Pos.y = (float)(length * Math.Sin(speed * 0.01 + tanValue));
        Pos.x = (float)(length * Math.Cos(speed * 0.01 + tanValue));
        return Pos + headPos;
    }

    void SetStartPos()//通过弧度值和长度计算当前向量
    {
        StartPos.x = (float)(length * Math.Cos(rad));
        StartPos.y = (float)(length * Math.Sin(rad));
    }
    public void SetSpeed() 
    {
        if(Last!=null)
        speed = Last.speed + s_speed;
        else
        speed = s_speed;
    }
    public void SetHeadPos(Vector3 p) => headPos = p;
    public Vector3 SetEndPos()=> endPos = headPos+StartPos;

    public void HideLine()//隐藏该线条
    {
        //// 获取材质的颜色
        //Color color = lineMaterial.color;
        //// 将透明度设置为0，使线条完全透明
        //color.a = 0f;
        //// 将修改后的颜色重新赋值给材质
        //lineMaterial.color = color;
    }
}
