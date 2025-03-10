using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;

public class Line : MonoBehaviour
{
    public Vector3 headPos;
    Vector3 endPos;

    Vector3 Pos;//当前向量
    Vector3 StartPos;//初始向量
    public double rad;//向量的弧度值

    public double P;//长度
    public Line Last;

    public LineRenderer lineRenderer;
    public LineRenderer trailRenderer;//轨迹渲染
    public float s_speed;//设定相对角速度 rad/s
    [SerializeField]float speed;//实际角速度（相对坐标系）

    private List<Vector3> trailPositions; // 轨迹点列表
    public int maxTrailPoints = 1000;//轨迹点上限
    void Awake()
    {
        SetStartPos();
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trailPositions = new List<Vector3>();
        //初始长度初始向量计算与实际速度计算
        Pos = StartPos;//赋值当前向量

        //endPos = headPos + StartPos;//计算初始点坐标
    }
    private void FixedUpdate()
    {
        Turning();//旋转
        UpdateTrail();//轨迹描绘
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
        Pos.y = (float)(P * Math.Sin(speed * 0.01 + tanValue));
        Pos.x = (float)(P * Math.Cos(speed * 0.01 + tanValue));
        return Pos + headPos;
    }

    void SetStartPos()//通过弧度值和长度计算当前向量
    {
        StartPos.x = (float)(P * Math.Cos(rad));
        StartPos.y = (float)(P * Math.Sin(rad));
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
}
