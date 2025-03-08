using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : SingleTon<MapManager>
{
    public MapNode currentNode;
    public List<MapNode> nodes;
    public List<MapNode> CityNodes;
    public List<MapNode> ShrineNodes;
    public List<MapNode> InnNodes;
    public List<MapNode> ShopNodes;
    public NodeCreater creater;
    [SerializeField] private Button enterNode;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        enterNode.onClick.AddListener(EnterNode);
    }
    private void OnEnable()
    {
        EventManager.nextDay += UpdarteDayData;
    }
    private void OnDisable()
    {
        EventManager.nextDay -= UpdarteDayData;
    }
    private void EnterNode()
    {
        currentNode.GetComponent<BuildingInNode>().Enter();
        //根据当前节点进入对应场景
    }
    public void TransPlace(MapNode enterNode)
    {
        if (!DecMove()) return;
        if (currentNode != null)
        {
            if (!CanReach(enterNode))
                return;
            currentNode.Exit();
        }
        enterNode.Enter();
        EventManager.UpdateMapUi();
    }
    private bool DecMove()
    {
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
        EventManager.SaveGameData();
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
public enum NodeStyle
{
    Shrine,
    Shop,
    Inn,
    City
}

