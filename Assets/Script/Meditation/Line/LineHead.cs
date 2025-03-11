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
        //因为使用局部坐标相当于是在父物体的坐标上
        Head.Traversal("Refresh", null);

    }
    
}
