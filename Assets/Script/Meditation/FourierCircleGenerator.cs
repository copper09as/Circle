using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourierCircleGenerator : MonoBehaviour
{
    [Header("����Ҷ����")]
    public int numberOfCircles = 5; // ���ӵ�Բ����
    public float baseRadius = 1f;    // �����뾶
    public float speedMultiplier = 1f; // �����ٶ�

    [Header("�켣����")]
    public int maxTrailPoints = 100; // ���켣����
    public LineRenderer lineRenderer;

    private List<Vector3> trailPositions = new List<Vector3>();
    private Vector3 currentPosition;

    void Start()
    {
        lineRenderer.positionCount = 0;
        currentPosition = Vector3.zero;
    }

    void Update()
    {
        // ���㵱ǰ֡��λ��
        Vector3 newPosition = CalculateFourierPosition(Time.time * speedMultiplier);

        // ���¹켣
        trailPositions.Add(newPosition);
        if (trailPositions.Count > maxTrailPoints)
        {
            trailPositions.RemoveAt(0);
        }

        // ����LineRenderer
        lineRenderer.positionCount = trailPositions.Count;
        lineRenderer.SetPositions(trailPositions.ToArray());

        currentPosition = newPosition;
    }

    // ����Ҷλ�ü��㣨ʾ���������ֽ⣩
    Vector3 CalculateFourierPosition(float t)
    {
        Vector3 position = Vector3.zero;
        for (int n = 1; n <= numberOfCircles; n++)
        {
            float radius = baseRadius / n; // ���ȣ�ʾ����ʽ��
            float angle = t * n;           // ���ٶ���������
            Vector3 offset = new Vector3(
                radius * Mathf.Cos(angle),
                radius * Mathf.Sin(angle),
                0
            );
            position += offset;
        }
        return position;
    }

    // ��ѡ����Scene��ͼ�л��Ƶ���Բ
    void OnDrawGizmos()
    {
        Vector3 prevPos = Vector3.zero;
        for (int n = 1; n <= numberOfCircles; n++)
        {
            float radius = baseRadius / n;
            Gizmos.color = Color.HSVToRGB(n / (float)numberOfCircles, 1, 1);
            Gizmos.DrawWireSphere(prevPos, radius);
            prevPos += new Vector3(
                radius * Mathf.Cos(Time.time * speedMultiplier * n),
                radius * Mathf.Sin(Time.time * speedMultiplier * n),
                0
            );
        }
    }
}
