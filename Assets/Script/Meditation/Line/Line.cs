using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;

public class Line : MonoBehaviour
{
    public Vector3 headPos;
    Vector3 endPos;

    Vector3 Pos;//��ǰ����
    Vector3 StartPos;//��ʼ����
    public double rad;//�����Ļ���ֵ

    public double P;//����
    public Line Last;

    public LineRenderer lineRenderer;
    public LineRenderer trailRenderer;//�켣��Ⱦ
    public float s_speed;//�趨��Խ��ٶ� rad/s
    [SerializeField]float speed;//ʵ�ʽ��ٶȣ��������ϵ��

    private List<Vector3> trailPositions; // �켣���б�
    public int maxTrailPoints = 1000;//�켣������
    void Awake()
    {
        SetStartPos();
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trailPositions = new List<Vector3>();
        //��ʼ���ȳ�ʼ����������ʵ���ٶȼ���
        Pos = StartPos;//��ֵ��ǰ����

        //endPos = headPos + StartPos;//�����ʼ������
    }
    private void FixedUpdate()
    {
        Turning();//��ת
        UpdateTrail();//�켣���
    }


    public void Turning()
    {

        if (Last != null)//��HeadPos��ֵ
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

    void SetStartPos()//ͨ������ֵ�ͳ��ȼ��㵱ǰ����
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
