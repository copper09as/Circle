using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using MainGame;
using Npc;
public class MapNode : MonoBehaviour
{
    public bool CanGet = true;
    public bool isLoacte;
    public List<MapNode> adjancentNode = new List<MapNode>();
    public List<string> npcName;
    public List<Npc.Npc> stayNpc = new List<Npc.Npc>();
    private LineRenderer lineRenderer;
    public Vector2Int transPos;
    public bool collapsed;
    public NodeCreater creater;
    [SerializeField] private Material material;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawLine();
    }
    public void DrawLine()
    {
        lineRenderer.material = material;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;

        // ���� LineRenderer �ĵ���
        lineRenderer.positionCount = adjancentNode.Count * 2;

        // ��ʼ������
        int index = 0;

        // �������ڽڵ㲢��������
        foreach (var node in adjancentNode)
        {
            lineRenderer.SetPosition(index, new Vector3(transform.position.x, transform.position.y, 0));
            lineRenderer.SetPosition(index + 1, new Vector3(node.gameObject.transform.position.x, node.gameObject.transform.position.y, 0));
            index += 2;
        }
    }
    private void OnMouseDown()
    {
        if (State.Instance.currentState != GameState.Map)
            return;
        MapManager.Instance.TransPlace(this,false);
    }
    public void AddAdj(MapNode node)//����ڵ�
    {
        if (adjancentNode.Contains(node))
            return;
        if (node == this) return;
        adjancentNode.Add(node);
        node.adjancentNode.Add(this);
    }
    public void AddNpc(Npc.Npc npc)
    {
        if (npc == null) Debug.LogError("null npc");
        stayNpc.Add(npc);
        npcName.Add(npc.npcName);
    }
    public void RemoveAdj(MapNode node)//�Ƴ�Ŀ��ڵ�
    {
        if (!adjancentNode.Contains(node))
            return;
        if (node == this) return;
        adjancentNode.Remove(node);
        node.adjancentNode.Remove(this);
    }
    public void Enter(bool IsSupper)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        if (!IsSupper &&GetComponent<NodeEvent>().Day>0) 
            GetComponent<NodeEvent>().EventTrig();
        MapManager.Instance.currentNode = this;
        GetComponent<SpriteRenderer>().color = Color.blue;
        foreach (var node in adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    public void Exit()
    {
        foreach (var node in adjancentNode)
        {
            node.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //EventManager.NextDay();
    }
}

