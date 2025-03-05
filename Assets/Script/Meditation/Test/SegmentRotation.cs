using UnityEngine;

public class SegmentRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // 初始转速
    public float maxRotationSpeed = 30f; // 最大转速限制
    public float radius = 1f; // 齿轮半径
    public SegmentRotation nextGear; // 联动下一个齿轮

    private Vector3 startPos;
    private Vector3 endPos;
    private LineRenderer lineRenderer;
    private float currentAngle;

    public void Initialize(LineRenderer lr, SegmentRotation next)
    {
        lineRenderer = lr;
        nextGear = next;
        startPos = lineRenderer.GetPosition(0);
        endPos = lineRenderer.GetPosition(1);
        radius = Vector3.Distance(startPos, endPos);
    }

    void Update()
    {
        // 限制转速范围
        rotationSpeed = Mathf.Clamp(rotationSpeed, -maxRotationSpeed, maxRotationSpeed);

        // 更新自身旋转
        currentAngle = rotationSpeed * Time.deltaTime;
        UpdatePosition();

        // 联动下一个齿轮
        if (nextGear != null && PathGenerator.Instance != null)
        {
            nextGear.startPos = this.endPos;
            nextGear.rotationSpeed = -this.rotationSpeed * (radius / nextGear.radius) * PathGenerator.Instance.sizeSpeedRatio;
            nextGear.UpdatePosition();
        }
    }

    public void UpdatePosition()
    {
        // 计算终点新位置
        Vector3 newEnd = startPos + Quaternion.Euler(0, 0, currentAngle) * (endPos - startPos);

        // 更新线段显示
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, newEnd);
        endPos = newEnd;

        // 更新傅里叶数据
        FourierSystem.Instance.RecordPosition(transform.GetSiblingIndex(), endPos);
    }
}