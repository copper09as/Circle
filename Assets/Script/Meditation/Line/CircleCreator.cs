using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCreator : MonoBehaviour
{
    public List<Line> lines;
    public Vector3 HeadPos;
    private void Start()
    {

        int i = 0;
        for(;i<= lines.Count-1; i++)
        {
            //   if (i != 0)
            //      { lines[i].SetHeadPos(lines[i - 1].endPos);Debug.Log(lines.Count); }
            //   else lines[0].SetHeadPos(new Vector3_(HeadPos));
            if (i != 0)
            { lines[i].Last = lines[i-1]; Debug.Log(lines.Count); }
            
        }

    }
}
