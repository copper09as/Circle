using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FourierUICircle : MaskableGraphic
{
    [Header("傅里叶参数")]
    public int circleCount = 3;     // 叠加的圆数量
    public float baseRadius = 50f;  // 基础半径（单位：像素）
    public float speed = 1f;        // 旋转速度

    [Header("轨迹设置")]
    public int maxTrailPoints = 100; // 最大轨迹点数
    public float trailWidth = 2f;    // 轨迹线宽

    private List<Vector2> trailPositions = new List<Vector2>();
    private Vector2 currentPosition;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        DrawCircles(vh);
        DrawTrail(vh);
    }

    // 绘制傅里叶圆和轨迹
    void DrawCircles(VertexHelper vh)
    {
        Vector2 center = rectTransform.rect.center;
        Vector2 prevPos = center;

        // 绘制每个圆
        for (int i = 0; i < circleCount; i++)
        {
            float radius = baseRadius / (i + 1);
            float angle = Time.time * speed * (i + 1);
            Vector2 offset = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            // 绘制圆环
            AddCircle(vh, prevPos, radius, Color.HSVToRGB(i / (float)circleCount, 1, 1));
            prevPos += offset;
        }

        // 记录当前位置
        currentPosition = prevPos;
        trailPositions.Add(currentPosition);
        if (trailPositions.Count > maxTrailPoints)
        {
            trailPositions.RemoveAt(0);
        }
    }

    // 绘制轨迹线
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

    // 工具方法：添加圆形顶点
    void AddCircle(VertexHelper vh, Vector2 center, float radius, Color color, int segments = 36)
    {
        int vertexIndex = vh.currentVertCount;
        float angleStep = 360f / segments;

        // 生成顶点
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 pos = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            vh.AddVert(pos, color, Vector2.zero);
        }

        // 生成三角形
        for (int i = 1; i <= segments; i++)
        {
            vh.AddTriangle(vertexIndex, vertexIndex + i, vertexIndex + i % segments + 1);
        }
    }

    // 工具方法：添加线段
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
        SetVerticesDirty(); // 每帧触发重绘
    }
}
