using UnityEngine;

public class TrailFollower : MonoBehaviour
{
    public Transform target; // Ŀ�����壨�߶ε�ĩ�ˣ�

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position; // ����βԤ�������Ŀ��
        }
    }
}
