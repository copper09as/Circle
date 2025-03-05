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
    //public Vector3_ HeadPos//�������ǳ����
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
    public float speed;//���ٶ� rad/s
    
    private List<Vector3> trailPositions; // �켣���б�
    public int maxTrailPoints =1000;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trailPositions = new List<Vector3>();
        GetP();

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
        lineRenderer.SetPosition(0,headPos);
        endPos = Calculation();
        lineRenderer.SetPosition(1, endPos);
    }
    private void UpdateTrail()
    {
        // ����µ㲢��������
        trailPositions.Add(endPos);
        if (trailPositions.Count > maxTrailPoints)
        {
            trailPositions.RemoveAt(0);
        }

        // ���¹켣LineRenderer
        trailRenderer.positionCount = trailPositions.Count;
        trailRenderer.SetPositions(trailPositions.ToArray());
    }
    public Vector3 Calculation()
    {
        Vector3 V  = new();
        Vector3 Pos = endPos - headPos;
        double tanValue = Math.Atan2(Pos.y,Pos.x);
        V.y = (float)(P * Math.Sin(speed * 0.01+tanValue));
        V.x = (float)(P * Math.Cos(speed * 0.01 + tanValue));
        return V + headPos;
    }

    public void GetP()
    {
        Vector3 Pos = endPos - headPos;
        P = Math.Sqrt(Math.Pow(Pos.x, 2.0) + Math.Pow(Pos.y, 2.0));
    }
}
