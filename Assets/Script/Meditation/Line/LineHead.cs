using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineHead : MonoBehaviour
{
    public Line Head;
    void Awake()
    {
        Head = GetComponent<Line>();
        Intialize();
    }
    void Intialize()
    {
        Debug.Log(transform.position);
        Head.SetHeadPos(Vector3.zero);
        //��Ϊʹ�þֲ������൱�����ڸ������������
        Head.Traversal("Refresh", null);

    }
    
}
