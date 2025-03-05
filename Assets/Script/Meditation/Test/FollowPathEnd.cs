using UnityEngine;

public class SegmentTailFollow : MonoBehaviour
{
    private GameObject tailObject;

    public void Initialize(GameObject tail)
    {
        tailObject = tail;
    }

    void Update()
    {
        // ��β�����������ת
        if (tailObject != null)
        {
            tailObject.transform.position = transform.GetChild(0).position; // ĩβλ��
            tailObject.transform.rotation = transform.rotation; // ������ת
        }
    }
}
