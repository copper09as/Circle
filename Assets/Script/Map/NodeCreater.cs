
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class NodeCreater : MonoBehaviour
{

    [SerializeField] private List<MapNode> nodes;
    [SerializeField] private List<MapNode> collapsedNodes;
    [SerializeField] private int DeleteCount;
    [Header("节点最大连线数")]
    [SerializeField] private int maxAdjNode;
    [Header("节点最小连线数")]
    [SerializeField] private int minAdjNode;
    [Header("节点最大连接数")]
    [SerializeField] private int maxAdj;
    [Header("节点最小连接数")]
    [SerializeField] private int minAdj;
    [Header("地图种子")]
    [SerializeField] private int MapSeed;
    [Header("节点长宽数量")]
    [SerializeField] private int NodeWidth;
    [SerializeField] private int NodeHeight;
    [Header("节点初始生成地偏移度")]
    [SerializeField] private float NodesOffestX;
    [SerializeField] private float NodesOffestY;
    [Header("节点偏移度")]
    [SerializeField] private float NodeX;
    [SerializeField] private float NodeY;
    [Header("节点间距")]
    [SerializeField] private float nodeRange;
    private Dictionary<int, int> collapsedXnode = new Dictionary<int, int>();
    private Dictionary<int, int> collapsedYnode = new Dictionary<int, int>();
    private int collapsedXnodeCount;
    private int collapsedYnodeCount;

    IEnumerator Start()
    {
        Random.InitState(MapSeed);
        yield return CreateNodeFirst(); // 等待节点初始化完成
        StartCoroutine(RandomNodeFirst(DeleteCount));
    }
    private IEnumerator CreateNodeFirst()
    {
        List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();
        for (int i = 0; i < NodeWidth; i++)
        {
            for (int j = 0; j < NodeHeight; j++)
            {
                int x = i;
                int y = j;

                Addressables.InstantiateAsync("MapNode").Completed += handle =>
                {
                    handle.Result.transform.SetParent(transform, false);
                    SpriteRenderer spriteRenderer = handle.Result.GetComponent<SpriteRenderer>();
                    MapNode node = handle.Result.GetComponent<MapNode>();
                    Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
                    Vector2 position = new Vector2(x * spriteSize.x * nodeRange + NodesOffestX, -y * spriteSize.y * nodeRange + NodesOffestY);
                    handle.Result.transform.position = position;
                    node.transPos = new Vector2Int(x, y);
                    node.collapsed = false;
                    node.gameObject.name = node.transPos.ToString();
                    handles.Add(handle);
                    nodes.Add(node);
                };
            }
        }
        yield return new WaitUntil(() => nodes.Count == NodeWidth * NodeHeight);
    }
    private void CreateNodeSecond()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            for (int j = i + 1; j < nodes.Count; j++)
            {
                var node2 = nodes[j];

                // 计算平方距离
                float dx = node.transPos.x - node2.transPos.x;
                float dy = node.transPos.y - node2.transPos.y;
                float sqrDistance = dx * dx + dy * dy;

                // 检查是否相邻
                if (sqrDistance <= 2 && i != j)
                {
                    node.adjancentNode.Add(node2);
                    node2.adjancentNode.Add(node);
                }

            }
        }
    }
    private IEnumerator RandomNodeFirst(int times)
    {
        int RandomTimes = 0;
        int initTimes = times;
        while (times > 0)
        {
            yield return null;
            int index = Random.Range(0, nodes.Count);
            var node = nodes[index];
            int keyX = node.transPos.x;
            int keyY = node.transPos.y;
            bool canRemove = true;
            DetectDic(collapsedXnode, NodeWidth, ref canRemove, keyX);
            DetectDic(collapsedYnode, NodeHeight, ref canRemove, keyY);
            /*if (collapsedXnode.ContainsKey(keyX))
            {
                if (collapsedXnode[keyX] >= NodeWidth - 1)
                {
                    canRemove = false;
                    Debug.Log("X无法形成连通图");
                }
                else
                {
                    collapsedXnode[keyX] += 1;
                }
            }
            else
            {
                collapsedXnode.Add(keyX, 1);
            }
            if (collapsedYnode.ContainsKey(keyY))
            {
                if (collapsedYnode[keyY] >= NodeHeight - 1)
                {
                    canRemove = false;
                    Debug.Log("Y无法形成连通图");
                }
                else
                {
                    collapsedYnode[keyY] += 1;
                }
            }
            else
            {
                collapsedYnode.Add(keyY, 1);
            }*/
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
            if (RandomTimes > initTimes * 3)
            {
                Debug.LogError("循环次数过多");
            }
            Debug.Log(index);

        }
        CreateNodeSecond();
        RandomNodeSecond();
        RandomNodeThird();
    }
    private void RandomNodeSecond()
    {
        List<MapNode> nodesToRemove = new List<MapNode>();

        // 遍历 nodes 列表
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];

            // 检查相邻节点数量
            if (node.adjancentNode.Count >= maxAdj || node.adjancentNode.Count <= minAdj)
            {
                foreach (var adjNode in node.adjancentNode)
                {
                    adjNode.adjancentNode.Remove(node);
                }
                node.collapsed = true;
                node.gameObject.SetActive(false);
                nodesToRemove.Add(node); // 将节点添加到待移除列表
                collapsedNodes.Add(node);
            }
        }

        // 移除需要折叠的节点
        foreach (var node in nodesToRemove)
        {
            nodes.Remove(node);
        }
        /*foreach(var node in collapsedNodes)
        {
            Destroy(node.gameObject);
        }*/

    }
    private void RandomNodeThird()
    {
        int index = 0;
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
        for (int i = 0; i < nodes.Count; i++)
        {

            if (nodes[i].adjancentNode.Count <= minAdjNode)
            {
                var connectNode = nodes.Find(n => (Mathf.Abs(nodes[i].transPos.x - n.transPos.x) + Mathf.Abs(nodes[i].transPos.y - n.transPos.y) > 2) && !nodes[i].adjancentNode.Contains(n));
                if (connectNode != null)
                {
                    connectNode.adjancentNode.Add(nodes[i]);
                    nodes[i].adjancentNode.Add(connectNode);
                }

            }
        }
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
            nodes[i].DrawLine();
            if (nodes[i].adjancentNode.Count >= 3)
            {
                index = i;
            }
        }
        foreach (var node in nodes)
        {
            IsRemovalSafe(node.transPos);
        }
        MapManager.Instance.TransPlace(nodes[index]);


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

        // 选择第一个未被删除的节点作为起点
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
        Debug.LogWarning((visited.Count >= (nodes.Count)/2) + position.ToString());

        return visited.Count >= (nodes.Count) / 2;
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
