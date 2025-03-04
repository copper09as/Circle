
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class NodeCreater : MonoBehaviour
{
    [SerializeField] private int NodeWidth;
    [SerializeField] private int NodeHeight;
    [SerializeField] private List<MapNode> nodes; 
    [SerializeField] private List<MapNode> collapsedNodes;
    [SerializeField] private int DeleteCount;
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
    void Start()
    {
        //Random.InitState(StaticResource.seed);
        CreateNodeFirst();
        StartCoroutine(RandomNodeFirst(DeleteCount));
        
    }

    private bool IsOverStep(MapNode Node, MapNode AdjNode)
    {
        return ((Node.transPos.x - AdjNode.transPos.x) > 0 && (Node.transPos.y - AdjNode.transPos.y > 0));
    }
    private bool IsAdj(MapNode Node, MapNode AdjNode)
    {
        return (Mathf.Abs((Node.transPos.x - AdjNode.transPos.x)) <= 1 && Mathf.Abs(Node.transPos.y - AdjNode.transPos.y) <= 1);
    }
    private bool AddNodeAdj(MapNode Node,MapNode AdjNode)
    {
        return IsAdj(Node,AdjNode)&&IsOverStep(Node,AdjNode);
    }
    private void CreateNodeFirst()
    {
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
                    Vector2 position = new Vector2(x * spriteSize.x* nodeRange + NodesOffestX, -y * spriteSize.y* nodeRange + NodesOffestY);
                    handle.Result.transform.position = position;
                    node.transPos = new Vector2Int(x, y);
                    node.collapsed = false;
                    nodes.Add(node);
                };
            }
        }
    }
    private void CreateNodeSecond()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            for (int j = i+1; j < nodes.Count; j++)
            {
                var node2 = nodes[j];

                // 计算平方距离
                float dx = node.transPos.x - node2.transPos.x;
                float dy = node.transPos.y - node2.transPos.y;
                float sqrDistance = dx * dx + dy * dy;

                // 检查是否相邻
                if (sqrDistance <= 2&&i!=j)
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
        while(times>0)
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
            if (RandomTimes > 60)
            {
                Debug.LogError("循环次数过多");
            }
            Debug.LogWarning(canRemove.ToString());
            Debug.Log(times);
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
            if (node.adjancentNode.Count >= 7 || node.adjancentNode.Count == 0)
            {
                foreach(var adjNode in node.adjancentNode)
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
        foreach(var node in collapsedNodes)
        {
            Destroy(node.gameObject);
        }
        
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
        for(int i = 0;i<nodes.Count;i++)
        {
            nodes[i].DrawLine();
            if (nodes[i].adjancentNode.Count >= 3)
            {
                index = i;
            }

        }

        MapManager.Instance.TransPlace(nodes[index]);
    }
    private void DetectDic(Dictionary<int,int>posDic,int range,ref bool canRemove,int key)
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
}
