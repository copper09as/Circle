using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;

public class Line : MonoBehaviour
{
    public Vector3 headPos;
    public Vector3 endPos;

    Vector3 Pos;//��ǰ����
    Vector3 StartPos;//��ʼ����

    private double P;//����
    public Line Last;

    public LineRenderer lineRenderer;
    public LineRenderer trailRenderer;//�켣��Ⱦ
    public float s_speed;//�趨��Խ��ٶ� rad/s
    float speed;//ʵ�ʽ��ٶȣ��������ϵ��

    private List<Vector3> trailPositions; // �켣���б�
    public int maxTrailPoints = 1000;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trailPositions = new List<Vector3>();
        SetP();//��ʼ���ȳ�ʼ����������ʵ���ٶȼ���
        Pos = StartPos;//��ֵ��ǰ����
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
        double tanValue = Math.Atan2(Pos.y, Pos.x);//�������ڵĻ���ֵ
        Pos.y = (float)(P * Math.Sin(speed * 0.01 + tanValue));
        Pos.x = (float)(P * Math.Cos(speed * 0.01 + tanValue));
        return Pos + headPos;
    }

    void SetP()
    {
        StartPos = endPos - headPos;//�����ʼ����
        P = Math.Sqrt(Math.Pow(StartPos.x, 2.0) + Math.Pow(StartPos.y, 2.0));//�����ʼ����
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
