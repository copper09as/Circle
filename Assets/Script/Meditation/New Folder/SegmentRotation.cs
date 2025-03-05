using UnityEngine;

public class SegmentRotation : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float rotationSpeed=0.5f; // 旋转速度（每秒旋转的角度）
    private float currentAngle = 0f; // 当前旋转角度

    public void Initialize(LineRenderer lineRenderer, float rotationSpeed)
    {
        this.lineRenderer = lineRenderer;
        this.rotationSpeed = rotationSpeed;
    }

    void Update()
    {
        // 以恒定速度旋转
        currentAngle = rotationSpeed * Time.deltaTime; // 每秒旋转一定的角度

        if (currentAngle > 360f) currentAngle -= 360f;

        // 获取起点和终点的位置
        Vector3 pivot = lineRenderer.GetPosition(0); // 起始点
        Vector3 endPos = lineRenderer.GetPosition(1); // 终点

        // 旋转终点
        Vector3 rotatedEndPos = RotatePoint(endPos, pivot, currentAngle);
        lineRenderer.SetPosition(1, rotatedEndPos);
    }

    Vector3 RotatePoint(Vector3 point, Vector3 pivot, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        // 计算旋转后的点
        float dx = point.x - pivot.x;
        float dy = point.y - pivot.y;

        float rotatedX = pivot.x + (cos * dx - sin * dy);
        float rotatedY = pivot.y + (sin * dx + cos * dy);

        return new Vector3(rotatedX, rotatedY, point.z);
    }
}