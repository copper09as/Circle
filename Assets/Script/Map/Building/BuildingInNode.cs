using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingInNode:MonoBehaviour
{
    public bool CanReach = true;
    public abstract void Enter();
}
