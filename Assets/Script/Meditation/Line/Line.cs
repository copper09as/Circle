using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;

public class Line : MonoBehaviour
{
    public Vector3 headPos;
    public Vector3 endPos;

    Vector3 Pos;//当前向量
    Vector3 StartPos;//初始向量

    private double P;//长度
    public Line Last;

    public LineRenderer lineRenderer;
    public LineRenderer trailRenderer;//轨迹渲染
    public float s_speed;//设定相对角速度 rad/s
    float speed;//实际角速度（相对坐标系）

    private List<Vector3> trailPositions; // 轨迹点列表
    public int maxTrailPoints = 1000;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trailPositions = new List<Vector3>();
        SetP();//初始长度初始向量计算与实际速度计算
        Pos = StartPos;//赋值当前向量
    }
    private void FixedUpdate()
    {
        Turning();
        UpdateTrail();
    }


    public void Turning()
    {

        if (Last != null)
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

    void SetP()
    {
        StartPos = endPos - headPos;//计算初始向量
        P = Math.Sqrt(Math.Pow(StartPos.x, 2.0) + Math.Pow(StartPos.y, 2.0));//计算初始长度
    }
    public float GetSpeed() => speed;
    public void SetSpeed() 
    {
        if(Last!=null)
        speed = Last.GetSpeed() + s_speed;
        else
        speed = s_speed;
    }
}
