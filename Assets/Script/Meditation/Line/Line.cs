using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;

public class Line : MonoBehaviour
{
    public Vector3 headPos;
    public Vector3 endPos;

    private double P;
    public Line Last;
    //public Vector3_ HeadPos//方便外界浅拷贝
    //{ get { return HeadPos; }
    // set { HeadPos = value; headPos = value.vector3; }
    //}
    //public Vector3_ EndPos
    //{
    //    get { return EndPos; }
    //    set { EndPos = value; endPos = value.vector3; }
    //}
    //public Vector3 FirstheadPos;
    //public Vector3 FirstendPos;

    public LineRenderer lineRenderer;
    public LineRenderer trailRenderer;
    public float speed;//角速度 rad/s
    
    private List<Vector3> trailPositions; // 轨迹点列表
    public int maxTrailPoints =1000;
    private void Awake()
    {
        //HeadPos = new Vector3_(FirstheadPos);
        //EndPos = new Vector3_(FirstendPos);
        lineRenderer = GetComponent<LineRenderer>();
        trailPositions = new List<Vector3>();
        GetP();
        //endPos = headPos+new Vector3(1f,1f,0);
    }
    void Update()
    {
        //var screenPos = Input.mousePosition;
        //lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10)));
        Turning();
        UpdateTrail();
    }
    //public void SetHeadPos(Vector3_ pos)=> HeadPos = pos; 

    public void Turning()
    {       
        lineRenderer.SetPosition(0,headPos);
        endPos = Calculation();
        if (Last != null)
            headPos = Last.endPos;
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
        Vector3 V  = new();
        Vector3 Pos = endPos - headPos;
        double tanValue = Math.Atan2(Pos.y,Pos.x);
        V.y = (float)(P * Math.Sin(speed * Time.deltaTime+tanValue));
        V.x = (float)(P * Math.Cos(speed * Time.deltaTime+tanValue));
        return V + headPos;
    }

    public void GetP()
    {
        Vector3 Pos = endPos - headPos;
        P = Math.Sqrt(Math.Pow(Pos.x, 2.0) + Math.Pow(Pos.y, 2.0));
    }
}
