using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public List<CircleEye> Eyes;

    void Start()
    {
        Eyes = GetComponentsInChildren<CircleEye>().ToList();
    }

    
    void Update()
    {
        
    }
}
