
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NodeBuilder
{
    bool collapsed;
    Transform parent;
    private float nodeRange;
    private Vector2 NodesOffest;
    List<MapNode> nodes;
    public NodeBuilder(bool collaspsed,Transform parent,float nodeRange, Vector2 NodesOffestX,List<MapNode>nodes)
    {
        this.parent = parent;
        this.collapsed = collaspsed;
        this.nodeRange = nodeRange;
        this.NodesOffest = NodesOffestX;
        this.nodes = nodes;
    }
    public void CreateNode(int x,int y,NodeCreater creater)
    {
        float NodesOffestX = NodesOffest.x;
        float NodesOffestY = NodesOffest.y;
        Addressables.InstantiateAsync("MapNode").Completed += handle =>
        {
            
            SpriteRenderer spriteRenderer = handle.Result.GetComponent<SpriteRenderer>();
            MapNode node = handle.Result.GetComponent<MapNode>();
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
            Vector2 position = new Vector2(x * spriteSize.x * nodeRange + NodesOffestX, -y * spriteSize.y * nodeRange + NodesOffestY);
            handle.Result.transform.SetParent(parent, false);
            handle.Result.transform.position = position;
            node.transPos = new Vector2Int(x, y);
            node.collapsed = collapsed;
            node.gameObject.name = node.transPos.ToString();
            node.creater = creater;
            nodes.Add(node);
        };
    }
    public void AddAdj(List<MapNode> nodes,MapNode node)
    {
        List<MapNode> tempNodes = nodes.Where(i => i != node && GetSqrDistance(node,i)<=2).ToList();
        foreach(var adjNode in tempNodes)
        {
            node.AddAdj(adjNode);
        }
    }
    public void RandomRemove(ref int RandomTimes,ref int times,List<MapNode> collapsedNodes,int index)
    {
        var node = nodes[index];

        bool canRemove = true;
        if (canRemove)
        {
            node.collapsed = true;
            node.gameObject.SetActive(false);
            nodes.RemoveAt(index);
            collapsedNodes.Add(node);
            times--;
        }
        else
        {
            RandomTimes += 1;
        }

    }
    public float GetSqrDistance(MapNode node, MapNode otherNode)
    {
        float dx = node.transPos.x - otherNode.transPos.x;
        float dy = node.transPos.y - otherNode.transPos.y;
        return dx * dx + dy * dy;
    }
    public void AddEvent()
    {
        foreach(var node in nodes)
        {
            var nodeEvent = node.AddComponent<NodeEvent>();
            nodeEvent.EventId = 3003;
            nodeEvent.Day = 999;
        }
    }
}
