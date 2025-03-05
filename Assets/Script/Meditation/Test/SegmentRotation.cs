using UnityEngine;

public class SegmentRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // ��ʼת��
    public float maxRotationSpeed = 30f; // ���ת������
    public float radius = 1f; // ���ְ뾶
    public SegmentRotation nextGear; // ������һ������

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
        // ����ת�ٷ�Χ
        rotationSpeed = Mathf.Clamp(rotationSpeed, -maxRotationSpeed, maxRotationSpeed);

        // ����������ת
        currentAngle = rotationSpeed * Time.deltaTime;
        UpdatePosition();

        // ������һ������
        if (nextGear != null && PathGenerator.Instance != null)
        {
            nextGear.startPos = this.endPos;
            nextGear.rotationSpeed = -this.rotationSpeed * (radius / nextGear.radius) * PathGenerator.Instance.sizeSpeedRatio;
            nextGear.UpdatePosition();
        }
    }

    public void UpdatePosition()
    {
        // �����յ���λ��
        Vector3 newEnd = startPos + Quaternion.Euler(0, 0, currentAngle) * (endPos - startPos);

        // �����߶���ʾ
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, newEnd);
        endPos = newEnd;

        // ���¸���Ҷ����
        FourierSystem.Instance.RecordPosition(transform.GetSiblingIndex(), endPos);
    }
}