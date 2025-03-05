using UnityEngine;

public class TrailFollower : MonoBehaviour
{
    public Transform target; // 目标物体（线段的末端）

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position; // 让拖尾预制体跟随目标
        }
    }
}
