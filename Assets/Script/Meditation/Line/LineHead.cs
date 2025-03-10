using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineHead : MonoBehaviour
{
    public List<Line> lines;

    void Awake()
    {
        lines = GetComponentsInChildren<Line>().ToList();
        Intialize();
    }
    void Intialize()
    {
        int i = 0;
        Debug.Log(transform.position);
        lines[0].SetHeadPos(transform.position);
        lines[0].SetEndPos();
        for (; i <= lines.Count - 1; i++)
        {
            if (i != 0)
            {
                lines[i].Last = lines[i - 1];
                lines[i].SetHeadPos(lines[i].SetEndPos());
            }
            lines[i].SetSpeed();//¼ÆËãËÙ¶È

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
