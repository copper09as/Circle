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

    Vector3 Pos;//��ǰ����
    Vector3 StartPos;//��ʼ����

    public double angle;//�Ƕ�
    double rad;//�����Ļ���ֵ
    [Header("��Խ��ٶ�")]
    public float s_speed;//�趨��Խ��ٶ� rad/s
    float speed;//ʵ�ʽ��ٶȣ��������ϵ��
    public double length;//����
    [Header("��һ����")]
    public Line Last;
    public List<Line> Nexts;

    [Header("����Ⱦ���͹켣��Ⱦ��")]
    public LineRenderer lineRenderer;
    //Material lineMaterial;
    public LineRenderer trailRenderer;//�켣��Ⱦ��

    private List<Vector3> trailPositions; // �켣���б�
    public int maxTrailPoints = 1000;//�켣������
    void Awake()
    {
        Nexts = new List<Line>();//��ʼ��
        rad = angle / 180 * 2 * Math.PI;//���㻡��ֵ
        SetStartPos();
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineMaterial = lineRenderer.material;
        trailRenderer.useWorldSpace = false;//ʹ�þֲ�����
        trailPositions = new List<Vector3>();
        //��ʼ���ȳ�ʼ����������ʵ���ٶȼ���
        Pos = StartPos;//��ֵ��ǰ����
        //endPos = headPos + StartPos;//�����ʼ������
        HideLine();//��������
        
    }
    private void FixedUpdate()
    {
        Turning();//��ת
        UpdateTrail();//�켣���
    }



    //�������ɭ��
    public void Traversal()
    {
        //��ʼִ��һЩ����
        foreach (Line line in Nexts)
        {
            line.Traversal();
        }//ִ�е���ʱ
        //��ʼִ��һЩ����
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
        Pos.y = (float)(length * Math.Sin(speed * 0.01 + tanValue));
        Pos.x = (float)(length * Math.Cos(speed * 0.01 + tanValue));
        return Pos + headPos;
    }

    void SetStartPos()//ͨ������ֵ�ͳ��ȼ��㵱ǰ����
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

    public void HideLine()//���ظ�����
    {
        //// ��ȡ���ʵ���ɫ
        //Color color = lineMaterial.color;
        //// ��͸��������Ϊ0��ʹ������ȫ͸��
        //color.a = 0f;
        //// ���޸ĺ����ɫ���¸�ֵ������
        //lineMaterial.color = color;
    }
}
