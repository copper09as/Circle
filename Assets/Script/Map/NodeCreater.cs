
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeCreater : MonoBehaviour
{
    [Header("节点大小")]
    [SerializeField] private float NodeScale;
    [Header("首次删除节点次数")]
    [SerializeField] private int DeleteCount;
    [Header("孤立节点判断条件")]
    [SerializeField] private float lonelyDec;
    [Header("是否开启环形修正")]
    [SerializeField] private bool isRound;
    [Header("当前存在的节点")]
    public List<MapNode> nodes;
    [Header("已坍缩的节点")]
    [SerializeField] private List<MapNode> collapsedNodes;
    [Header("是否可出现传送城")]
    [SerializeField] private bool isMagicCity;
    [Header("传送城判定参数")]
    [SerializeField] private float magicCityDis;
    [Header("是否可出现孤岛")]
    [SerializeField] private bool isLonely;
    [Header("节点最大连线数")]
    [SerializeField] private int maxAdjNode;
    [Header("节点最小连线数")]
    [SerializeField] private int minAdjNode;
    [Header("节点最大邻接数")]
    [SerializeField] private int maxAdj;
    [Header("节点最小邻接数")]
    [SerializeField] private int minAdj;
    [Header("地图种子")]
    [SerializeField] private int MapSeed;
    [Header("节点长宽数量")]
    [SerializeField] private int NodeWidth;
    [SerializeField] private int NodeHeight;
    [Header("节点初始生成地偏移度")]
    [SerializeField] private float NodesOffestX;
    [SerializeField] private float NodesOffestY;
    [Header("节点是否偏移")]
    [SerializeField] private bool isOffest;
    [Header("节点偏移度")]
    [SerializeField] private float NodeX;
    [SerializeField] private float NodeY;
    [Header("节点间距")]
    [SerializeField] private float nodeRange;


    private Dictionary<int, int> collapsedXnode = new Dictionary<int, int>();
    private Dictionary<int, int> collapsedYnode = new Dictionary<int, int>();
    private NodeBuilder nodeBuilder;
    IEnumerator Start()
    {
        nodeBuilder = new NodeBuilder(false, transform, nodeRange, new Vector2(NodesOffestX, NodesOffestY), nodes);
        Random.InitState(MapSeed);
        yield return CreateNodeFirst(); // 等待节点初始化完成
        StartCoroutine(RandomNodeFirst(DeleteCount));
        MapManager.Instance.nodes = this.nodes;
        foreach(var node in nodes)
        {
            int ranstyle = Random.Range(0, 4);
            var nodeTypyFactory = new NodeTypyFactory((NodeStyle)ranstyle,node);
            nodeTypyFactory.AddBuilding();
        }
        nodeBuilder.AddEvent();
    }
    private IEnumerator CreateNodeFirst()
    {
        for (int i = 0; i < NodeWidth; i++)
        {
            for (int j = 0; j < NodeHeight; j++)
            {
                nodeBuilder.CreateNode(i, j, this);
            }
        }
        yield return new WaitUntil(() => nodes.Count == NodeWidth * NodeHeight);
    }
    private void AddAdjNode()
    {


        foreach (var node in nodes)
        {
            nodeBuilder.AddAdj(nodes, node);
        }
    }
    public float GetSqrDistance(MapNode node, MapNode otherNode)
    {
        float dx = node.transPos.x - otherNode.transPos.x;
        float dy = node.transPos.y - otherNode.transPos.y;
        return dx * dx + dy * dy;
    }
    private IEnumerator RandomNodeFirst(int times)
    {
        int RandomTimes = 0;
        int initTimes = times;
        yield return null;
        while (times > 0)
        {

            int index = Random.Range(0, nodes.Count);
            var node = nodes[index];
            int keyX = node.transPos.x;
            int keyY = node.transPos.y;
            bool canRemove = true;
            if (isRound)
            {
                DetectDic(collapsedXnode, NodeWidth, ref canRemove, keyX);
                DetectDic(collapsedYnode, NodeHeight, ref canRemove, keyY);
            }
            nodeBuilder.RandomRemove(ref RandomTimes, ref times, collapsedNodes, index);
            /*if (canRemove)
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
            }*/
            if (RandomTimes > initTimes * 3)
            {
                Debug.LogWarning("循环次数过多");
                break;
            }

        }
        AddAdjNode();
        ControlNodeAdj();
        RandomNodeThird();
    }
    private void ControlNodeAdj()
    {
        List<MapNode> nodesToRemove = new List<MapNode>();

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];

            if (node.adjancentNode.Count >= maxAdj || node.adjancentNode.Count <= minAdj)
            {
                foreach (var adjNode in node.adjancentNode)
                {
                    adjNode.adjancentNode.Remove(node);
                }
                node.collapsed = true;
                node.gameObject.SetActive(false);
                nodesToRemove.Add(node);
                collapsedNodes.Add(node);
            }
        }

        foreach (var node in nodesToRemove)
        {
            nodes.Remove(node);
        }

    }
    private void MapNodeOffset()
    {
        foreach (var node in nodes)
        {
            // 生成随机偏移量
            float randomOffsetX = Random.Range(-NodeX, NodeX); // x 方向随机偏移
            float randomOffsetY = Random.Range(-NodeY, NodeY); // y 方向随机偏移

            // 设置节点的位置，并添加随机偏移
            node.transform.position = new Vector2(
            node.transform.position.x + randomOffsetX,
            node.transform.position.y + randomOffsetY
        );
        }
    }
    private void HelpLonelyNode()//控制邻接较少的节点
    {
        for (int i = 0; i < nodes.Count; i++)
        {

            if (nodes[i].adjancentNode.Count <= minAdjNode)
            {
                var connectNode = nodes.Find(n => GetSqrDistance(nodes[i],n) < ((NodeHeight * NodeHeight + NodeWidth * NodeWidth) / lonelyDec));
                if (connectNode != null)
                {
                    connectNode.AddAdj(nodes[i]);
   
                }
            }
        }
    }
    private void DeleteFriendNode()//删除邻接较多的节点
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].adjancentNode.Count >= maxAdjNode)
            {
                while (nodes[i].adjancentNode.Count >= maxAdjNode)
                {
                    int randomRemoveAdj = Random.Range(0, nodes[i].adjancentNode.Count);
                    nodes[i].adjancentNode[randomRemoveAdj].adjancentNode.Remove(nodes[i]);
                    nodes[i].adjancentNode.RemoveAt(randomRemoveAdj);
                }
            }
        }
    }
    private void RandomNodeThird()
    {
        if(isOffest)
            MapNodeOffset();
        HelpLonelyNode();
        DeleteFriendNode();
        LonelyCityDec();
        foreach (var node in nodes)
        {
            node.DrawLine();
        }
        if (nodes.Count > 0)
            MapManager.Instance.TransPlace(nodes[0]);
        else
            Debug.LogError("所有节点已坍缩，无法指定初始节点");
    }
    private void LonelyCityDec()
    {
        if (isLonely) return;
        foreach (var node in nodes)
        {
            int addIndext = nodes.Count - 1;
            var nodeRange = GetSqrDistance(node, nodes[addIndext]);
            while (!IsRemovalSafe(node.transPos) && addIndext > 0)
            {
                addIndext--;
                if (isMagicCity || (nodeRange <= (NodeHeight * NodeHeight + NodeWidth * NodeWidth) / magicCityDis))
                {
                    node.AddAdj(nodes[addIndext]);
                    continue;
                }
            }
        }
    }
    private void DetectDic(Dictionary<int, int> posDic, int range, ref bool canRemove, int key)
    {
        if (posDic.ContainsKey(key))
        {
            if (posDic[key] >= range - 1)
            {
                canRemove = false;
                Debug.Log("无法形成连通图");
            }
            else
            {
                posDic[key] += 1;
            }
        }
        else
        {
            posDic.Add(key, 1);
        }
    }
    private bool IsRemovalSafe(Vector2Int position)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        var startNode = nodes.Find(n => n.transPos == position);
        if (startNode == null) return false;

        queue.Enqueue(startNode.transPos);
        visited.Add(startNode.transPos);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var neighbor in GetNeighbors(current))
            {
                if (!visited.Contains(neighbor) && nodes.Exists(n => n.transPos == neighbor && !n.collapsed))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
        Debug.LogWarning((visited.Count > (nodes.Count) / 2) + position.ToString());

        return visited.Count > (nodes.Count) / 2;
    }
    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> tempNodes = new List<Vector2Int>();
        var obNode = nodes.Find(i => i.transPos == position);
        foreach (var node in obNode.adjancentNode)
        {
            tempNodes.Add(node.transPos);
        }
        return tempNodes;
    }
}
