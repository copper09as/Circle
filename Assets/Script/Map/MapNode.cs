using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public bool CanGet = true;
    public List<MapNode> adjancentNode = new List<MapNode>();
    private LineRenderer lineRenderer;
    [SerializeField] private Material material;
    private void Awake()
    {
        float ran = Random.Range(0f, 1f);
        Debug.Log(ran.ToString());
        lineRenderer = GetComponent<LineRenderer>();
        DrawLine();
    }

    private void DrawLine()
    {
        lineRenderer.material = material;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

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

    public void Enter()
    {
        MapManager.Instance.currentNode = this;
        foreach (var node in adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    public void Exit()
    {
        foreach (var node in adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}

