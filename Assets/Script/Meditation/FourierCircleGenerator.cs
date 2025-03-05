using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourierCircleGenerator : MonoBehaviour
{
    [Header("傅里叶参数")]
    public int numberOfCircles = 5; // 叠加的圆数量
    public float baseRadius = 1f;    // 基础半径
    public float speedMultiplier = 1f; // 整体速度

    [Header("轨迹绘制")]
    public int maxTrailPoints = 100; // 最大轨迹点数
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
        // 计算当前帧的位置
        Vector3 newPosition = CalculateFourierPosition(Time.time * speedMultiplier);

        // 更新轨迹
        trailPositions.Add(newPosition);
        if (trailPositions.Count > maxTrailPoints)
        {
            trailPositions.RemoveAt(0);
        }

        // 更新LineRenderer
        lineRenderer.positionCount = trailPositions.Count;
        lineRenderer.SetPositions(trailPositions.ToArray());

        currentPosition = newPosition;
    }

    // 傅里叶位置计算（示例：方波分解）
    Vector3 CalculateFourierPosition(float t)
    {
        Vector3 position = Vector3.zero;
        for (int n = 1; n <= numberOfCircles; n++)
        {
            float radius = baseRadius / n; // 幅度（示例公式）
            float angle = t * n;           // 角速度与阶数相关
            Vector3 offset = new Vector3(
                radius * Mathf.Cos(angle),
                radius * Mathf.Sin(angle),
                0
            );
            position += offset;
        }
        return position;
    }

    // 可选：在Scene视图中绘制调试圆
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
