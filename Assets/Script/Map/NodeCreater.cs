
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
    [Header("�ڵ����������")]
    [SerializeField] private int maxAdjNode;
    [Header("�ڵ���С������")]
    [SerializeField] private int minAdjNode;
    [Header("�ڵ����������")]
    [SerializeField] private int maxAdj;
    [Header("�ڵ���С������")]
    [SerializeField] private int minAdj;
    [Header("��ͼ����")]
    [SerializeField] private int MapSeed;
    [Header("�ڵ㳤������")]
    [SerializeField] private int NodeWidth;
    [SerializeField] private int NodeHeight;
    [Header("�ڵ��ʼ���ɵ�ƫ�ƶ�")]
    [SerializeField] private float NodesOffestX;
    [SerializeField] private float NodesOffestY;
    [Header("�ڵ�ƫ�ƶ�")]
    [SerializeField] private float NodeX;
    [SerializeField] private float NodeY;
    [Header("�ڵ���")]
    [SerializeField] private float nodeRange;
    private Dictionary<int, int> collapsedXnode = new Dictionary<int, int>();
    private Dictionary<int, int> collapsedYnode = new Dictionary<int, int>();
    private int collapsedXnodeCount;
    private int collapsedYnodeCount;

    IEnumerator Start()
    {
        Random.InitState(MapSeed);
        yield return CreateNodeFirst(); // �ȴ��ڵ��ʼ�����
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

                // ����ƽ������
                float dx = node.transPos.x - node2.transPos.x;
                float dy = node.transPos.y - node2.transPos.y;
                float sqrDistance = dx * dx + dy * dy;

                // ����Ƿ�����
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
                    Debug.Log("X�޷��γ���ͨͼ");
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
                    Debug.Log("Y�޷��γ���ͨͼ");
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
                Debug.LogError("ѭ����������");
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

        // ���� nodes �б�
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];

            // ������ڽڵ�����
            if (node.adjancentNode.Count >= maxAdj || node.adjancentNode.Count <= minAdj)
            {
                foreach (var adjNode in node.adjancentNode)
                {
                    adjNode.adjancentNode.Remove(node);
                }
                node.collapsed = true;
                node.gameObject.SetActive(false);
                nodesToRemove.Add(node); // ���ڵ���ӵ����Ƴ��б�
                collapsedNodes.Add(node);
            }
        }

        // �Ƴ���Ҫ�۵��Ľڵ�
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
            // �������ƫ����
            float randomOffsetX = Random.Range(-NodeX, NodeX); // x �������ƫ��
            float randomOffsetY = Random.Range(-NodeY, NodeY); // y �������ƫ��

            // ���ýڵ��λ�ã���������ƫ��
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
                Debug.Log("�޷��γ���ͨͼ");
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

        // ѡ���һ��δ��ɾ���Ľڵ���Ϊ���
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
