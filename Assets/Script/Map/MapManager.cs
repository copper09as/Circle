using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingleTon<MapManager>
{
    public MapNode currentNode;
    public List<MapNode> nodes;
    void Start()
    {

        float ran = Random.Range(0f, 1f);
        //currentNode.Enter();
        Debug.Log(ran.ToString());
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    void Update()
    {

    }

    public void TransPlace(MapNode enterNode)
    {
        if (currentNode == enterNode)
            return;
        if (currentNode != null)
        {
            if (!CanReach(enterNode))
                return;
            currentNode.Exit();
        }
        enterNode.Enter();
        StaticResource.day += 1;
        EventManager.UpdateMapUi();
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
