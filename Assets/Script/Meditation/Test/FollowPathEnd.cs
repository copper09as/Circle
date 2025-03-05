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
        // 让尾部物体跟随旋转
        if (tailObject != null)
        {
            tailObject.transform.position = transform.GetChild(0).position; // 末尾位置
            tailObject.transform.rotation = transform.rotation; // 跟随旋转
        }
    }
}
