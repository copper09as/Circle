using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FourierUICircle : MaskableGraphic
{
    [Header("����Ҷ����")]
    public int circleCount = 3;     // ���ӵ�Բ����
    public float baseRadius = 50f;  // �����뾶����λ�����أ�
    public float speed = 1f;        // ��ת�ٶ�

    [Header("�켣����")]
    public int maxTrailPoints = 100; // ���켣����
    public float trailWidth = 2f;    // �켣�߿�

    private List<Vector2> trailPositions = new List<Vector2>();
    private Vector2 currentPosition;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        DrawCircles(vh);
        DrawTrail(vh);
    }

    // ���Ƹ���ҶԲ�͹켣
    void DrawCircles(VertexHelper vh)
    {
        Vector2 center = rectTransform.rect.center;
        Vector2 prevPos = center;

        // ����ÿ��Բ
        for (int i = 0; i < circleCount; i++)
        {
            float radius = baseRadius / (i + 1);
            float angle = Time.time * speed * (i + 1);
            Vector2 offset = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            // ����Բ��
            AddCircle(vh, prevPos, radius, Color.HSVToRGB(i / (float)circleCount, 1, 1));
            prevPos += offset;
        }

        // ��¼��ǰλ��
        currentPosition = prevPos;
        trailPositions.Add(currentPosition);
        if (trailPositions.Count > maxTrailPoints)
        {
            trailPositions.RemoveAt(0);
        }
    }

    // ���ƹ켣��
    void DrawTrail(VertexHelper vh)
    {
        if (trailPositions.Count < 2) return;

        for (int i = 1; i < trailPositions.Count; i++)
        {
            Vector2 start = trailPositions[i - 1];
            Vector2 end = trailPositions[i];
            AddLine(vh, start, end, trailWidth, color);
        }
    }

    // ���߷��������Բ�ζ���
    void AddCircle(VertexHelper vh, Vector2 center, float radius, Color color, int segments = 36)
    {
        int vertexIndex = vh.currentVertCount;
        float angleStep = 360f / segments;

        // ���ɶ���
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 pos = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            vh.AddVert(pos, color, Vector2.zero);
        }

        // ����������
        for (int i = 1; i <= segments; i++)
        {
            vh.AddTriangle(vertexIndex, vertexIndex + i, vertexIndex + i % segments + 1);
        }
    }

    // ���߷���������߶�
    void AddLine(VertexHelper vh, Vector2 start, Vector2 end, float width, Color color)
    {
        Vector2 dir = (end - start).normalized;
        Vector2 perpendicular = new Vector2(-dir.y, dir.x) * width / 2f;

        int vertexIndex = vh.currentVertCount;
        vh.AddVert(start - perpendicular, color, Vector2.zero);
        vh.AddVert(start + perpendicular, color, Vector2.zero);
        vh.AddVert(end + perpendicular, color, Vector2.zero);
        vh.AddVert(end - perpendicular, color, Vector2.zero);

        vh.AddTriangle(vertexIndex, vertexIndex + 1, vertexIndex + 2);
        vh.AddTriangle(vertexIndex, vertexIndex + 2, vertexIndex + 3);
    }

    void Update()
    {
        SetVerticesDirty(); // ÿ֡�����ػ�
    }
}
