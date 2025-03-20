using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainGame;
using Npc;
public class MapManager : SingleTon<MapManager>
{
    public Character character;
    public MapNode currentNode;
    public List<MapNode> nodes;
    public List<MapNode> CityNodes;
    public List<MapNode> ShrineNodes;
    public List<MapNode> InnNodes;
    public List<MapNode> ShopNodes;
    public List<MapNode> SpecialNodes;
    public NodeCreater creater;
    [SerializeField] private PeopleProPanel peoplePanel;
    [SerializeField] private Button enterNode;
    [SerializeField] private Button peopleProfile;
    public Dictionary<string, System.Type> npcMap = new Dictionary<string, System.Type>();
    private void Awake()
    {
        npcMap.Add("Defector", typeof(Defector));
        npcMap.Add("BraveNpc", typeof(BraveNpc));
        npcMap.Add("LostChild", typeof(LostChild));
        npcMap.Add("Wizza", typeof(Wizza));
        npcMap.Add("Lost", typeof(Lost));
    }
    private void OnEnable()
    {
        EventManager.nextDay += UpdarteDayData;
        EventManager.saveGameData += SaveNodeEvent;
        enterNode.onClick.AddListener(EnterNode);
        peopleProfile.onClick.AddListener(DisplayLocalPeople);
    }
    private void OnDisable()
    {
        EventManager.nextDay -= UpdarteDayData;
        EventManager.saveGameData -= SaveNodeEvent;
    }
    public Npc.Npc AddNpc(MapNode node, string key)
    {
        Npc.Npc npcComponent = null;
        if (npcMap.TryGetValue(key, out System.Type componentType))
        {
            npcComponent = node.gameObject.AddComponent(componentType) as Npc.Npc;
            if (npcComponent != null)
            {
                return npcComponent;
            }
        }
        else
        {
            Debug.LogError($"未找到与关键字 '{key}' 关联的组件类型。");
        }
        return null;
    }
    private void EnterNode()
    {
        if (State.Instance.currentState != GameState.Map)
            return;
        currentNode.GetComponent<BuildingInNode>().Enter();
        //根据当前节点进入对应场景
    }
    public void TransPlace(MapNode enterNode,bool isLoad)//如果isLoad为真，则不消耗行动力
    {
        if (!DecMove(isLoad)) return;
        if (currentNode != null)
        {
            if (!CanReach(enterNode))
                return;
            currentNode.Exit();
        }
            enterNode.Enter(isLoad);

        EventManager.UpdateMapUi();
    }
    private bool DecMove(bool isLoad)
    {
        if (isLoad)
        {
            GameDataManager.Instance.move -= 0;
            return true;
        }
            
        if (GameDataManager.Instance.move > 0)
        {
            GameDataManager.Instance.move -= 1;
            return true;
        }
        return false;
    }
    private void UpdarteDayData()
    {

        creater.initNodePos = currentNode.transPos;
        GameDataManager.Instance.move = StaticResource.maxMove;
        GameDataManager.Instance.day += 1;

        foreach(var node in nodes)
        {
            node.GetComponent<BuildingInNode>().CanReach = true;
        }
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
    private void DisplayLocalPeople()
    {
        if(currentNode != null && State.Instance.currentState == GameState.Map)
        {
            Debug.Log(currentNode.stayNpc[0].npcName);
            peoplePanel.gameObject.SetActive(true);
            State.Instance.currentState = GameState.People;
        }
    }
    private void SaveNodeEvent()
    {
        GameDataManager.Instance.SaveEventData(nodes);
    }
}
  
public enum NodeStyle
{
    Shrine,
    Shop,
    Inn,
    City,
    MagicManager
}

