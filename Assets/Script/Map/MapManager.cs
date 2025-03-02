using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingleTon<MapManager>
{
    public MapNode currentNode;
    void Start()
    {
        Random.InitState(StaticResource.seed);
        float ran = Random.Range(0f, 1f);
        Debug.Log(ran.ToString());
    }

    void Update()
    {
        
    }
    public bool CanReach(MapNode EnterNode)
    {
        if (CanReachNode(EnterNode))
        {
            return true;
        }
        return false;
    }
    private bool CanReachNode(MapNode EnterNode)
    {
        return (currentNode.adjancentNode.Contains(EnterNode) && EnterNode.CanGet);
    }

}
