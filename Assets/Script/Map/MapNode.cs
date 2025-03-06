using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public bool CanGet = true;
    public List<MapNode> adjancentNode = new List<MapNode>();
    private LineRenderer lineRenderer;
    public Vector2Int transPos;
    public bool collapsed;
    public NodeCreater creater;
    public List<int> events;
    [SerializeField] private Material material;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawLine();
    }
    public void Init()
    {
        creater.nodes.Add(this);
    }
    public void DrawLine()
    {
        lineRenderer.material = material;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;

        // 设置 LineRenderer 的点数
        lineRenderer.positionCount = adjancentNode.Count * 2;

        // 初始化索引
        int index = 0;

        // 遍历相邻节点并绘制连线
        foreach (var node in adjancentNode)
        {
            lineRenderer.SetPosition(index, new Vector3(transform.position.x, transform.position.y, 0));
            lineRenderer.SetPosition(index + 1, new Vector3(node.gameObject.transform.position.x, node.gameObject.transform.position.y, 0));
            index += 2;
        }
    }
    private void OnMouseDown()
    {
        MapManager.Instance.TransPlace(this);
    }
    public void EventTrig()
    {
        MapEventManager.Instance.EffectTrid(GetRandomEventId());
    }
    public int GetRandomEventId()
    {
        return events[Random.Range(0, events.Count)];
    }
    public void AddAdj(MapNode node)
    {
        if (adjancentNode.Contains(node))
            return;
        if (node == this) return;
        adjancentNode.Add(node);
        node.adjancentNode.Add(this);
    }
    public void RemoveAdj(MapNode node)
    {
        if (!adjancentNode.Contains(node))
            return;
        if (node == this) return;
        adjancentNode.Remove(node);
        node.adjancentNode.Remove(this);
    }
    public void Enter()
    {
        MapManager.Instance.currentNode = this;
        GetComponent<SpriteRenderer>().color = Color.blue;
        foreach (var node in adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.green;
        }
        EventTrig();
    }
    public void Exit()
    {
        foreach (var node in adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}

