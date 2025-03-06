using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCreator : MonoBehaviour
{
    public List<Line> lines;
    public Vector3 HeadPos;
    private void Start()
    {
        Intialize();
        
    }
    void Intialize()
    {
        int i = 0;
        for (; i <= lines.Count - 1; i++)
        {
            if (i != 0)
            {
                lines[i].Last = lines[i - 1];
            }
            lines[i].SetSpeed();//


        }
    }
}
