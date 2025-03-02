using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingleTon<MapManager>
{
    public MapNode currentNode;
    public List<MapNode> nodes;
    void Start()
    {
        Random.InitState(StaticResource.seed);
        float ran = Random.Range(0f, 1f);
        Debug.Log(ran.ToString());
    }
    private void OnEnable()
    {
        EventManager.updateUi += TransColor;
    }
    private void OnDisable()
    {
        EventManager.updateUi -= TransColor;
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
    private void TransColor()
    {
        foreach (var node in currentNode.adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
