using UnityEngine;

public class SegmentRotation : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float rotationSpeed=0.5f; // ��ת�ٶȣ�ÿ����ת�ĽǶȣ�
    private float currentAngle = 0f; // ��ǰ��ת�Ƕ�

    public void Initialize(LineRenderer lineRenderer, float rotationSpeed)
    {
        this.lineRenderer = lineRenderer;
        this.rotationSpeed = rotationSpeed;
    }

    void Update()
    {
        // �Ժ㶨�ٶ���ת
        currentAngle = rotationSpeed * Time.deltaTime; // ÿ����תһ���ĽǶ�

        if (currentAngle > 360f) currentAngle -= 360f;

        // ��ȡ�����յ��λ��
        Vector3 pivot = lineRenderer.GetPosition(0); // ��ʼ��
        Vector3 endPos = lineRenderer.GetPosition(1); // �յ�

        // ��ת�յ�
        Vector3 rotatedEndPos = RotatePoint(endPos, pivot, currentAngle);
        lineRenderer.SetPosition(1, rotatedEndPos);
    }

    Vector3 RotatePoint(Vector3 point, Vector3 pivot, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        // ������ת��ĵ�
        float dx = point.x - pivot.x;
        float dy = point.y - pivot.y;

        float rotatedX = pivot.x + (cos * dx - sin * dy);
        float rotatedY = pivot.y + (sin * dx + cos * dy);

        return new Vector3(rotatedX, rotatedY, point.z);
    }
}