using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameDataManager : SingleTon<GameDataManager>,IDestroySelf
{
    [SerializeField] private DayData dayData;
    [SerializeField] private NodeData nodeData;
    [SerializeField] private NodeCreater nodeCreater;
    [SerializeField] private TitleUi titleUi;
    [SerializeField] private MapEventData eventData;

    public int move
    {
        get
        {
            if (dayData == null)
                dayData = new DayData()
                {
                    day = 1,
                    move = 2
                };

            return dayData.move;
        }
        set
        {
            dayData.move = value;
        }
    }
    public int day
    {
        get
        {
            return dayData.day;
        }
        set
        {
            dayData.day = value;
        }
    }
    private void Awake()
    {
        if (titleUi != null)
            titleUi.importantManager.Add(this);
        DontDestroyOnLoad(gameObject);
        LoadDayData();
        dayData = GameSave.LoadByJson<DayData>("DayData.json");
        nodeData = GameSave.LoadByJson<NodeData>("NodeData.json");
        eventData = GameSave.LoadByJson<MapEventData>("EventData.json");
    }
    private void OnEnable()
    {
        EventManager.saveGameData += SaveDayData;
        EventManager.saveGameData += SaveNodeData;
    }
    private void SaveDayData()
    {
        GameSave.SaveByJson("DayData.json", dayData);
        Debug.Log("测试天数存档功能");
    }
    private void LoadDayData()
    {
        try
        {
            day = dayData.day;
            move = dayData.move;
        }
        catch
        {
            dayData = new DayData
            {
                day = 1,
                move = StaticResource.maxMove
            };
            GameSave.SaveByJson("DayData.json", dayData);

        }
        EventManager.UpdateMapUi();
    }
    public void SaveNodeData()
    {

            if(nodeData == null)
        {
            nodeData = new NodeData();
            Debug.Log("新建nodeData");
        }
        if (nodeCreater == null)
            nodeCreater = GameObject.Find("Place").GetComponent<NodeCreater>();
        if (nodeCreater == null)
            return;
            nodeData.isOffest = nodeCreater.isOffest;
            nodeData.NodesOffestX = nodeCreater.NodesOffestX;
            nodeData.NodesOffestY = nodeCreater.NodesOffestY;
            nodeData.MapSeed = nodeCreater.MapSeed;
            nodeData.NodeScale = nodeCreater.NodeScale;
            nodeData.DeleteCount = nodeCreater.DeleteCount;
            nodeData.initNodePos = nodeCreater.initNodePos;
            nodeData.isLonely = nodeCreater.isLonely;
            nodeData.isMagicCity = nodeCreater.isMagicCity;
            nodeData.magicCityDis = nodeCreater.magicCityDis;
            nodeData.isRound = nodeCreater.isRound;
            nodeData.lonelyDec = nodeCreater.lonelyDec;
            nodeData.maxAdj = nodeCreater.maxAdj;
            nodeData.maxAdjNode = nodeCreater.maxAdjNode;
            nodeData.minAdj = nodeCreater.minAdj;
            nodeData.minAdjNode = nodeCreater.minAdjNode;
            nodeData.NodeHeight = nodeCreater.NodeHeight;
            nodeData.NodeWidth = nodeCreater.NodeWidth;
            nodeData.nodeRange = nodeCreater.nodeRange;
            nodeData.NodeX = nodeCreater.NodeX;
            nodeData.NodeY = nodeCreater.NodeY;
            GameSave.SaveByJson("NodeData.json", nodeData);
            Debug.Log("测试节点存档功能");
        
    }
    public void LoadNodeData(NodeCreater nodeCreater)
    {
        if (nodeCreater == null) nodeCreater = GameObject.Find("Place").GetComponent<NodeCreater>();
        Debug.Assert(nodeData != null, "读取地图存档失败");
        nodeCreater.isOffest = nodeData.isOffest;
        nodeCreater.NodesOffestX = nodeData.NodesOffestX;
        nodeCreater.NodesOffestY = nodeData.NodesOffestY;
        nodeCreater.MapSeed = nodeData.MapSeed;
        nodeCreater.NodeScale = nodeData.NodeScale;
        nodeCreater.DeleteCount = nodeData.DeleteCount;
        nodeCreater.initNodePos = nodeData.initNodePos;
        nodeCreater.isLonely = nodeData.isLonely;
        nodeCreater.isMagicCity = nodeData.isMagicCity;
        nodeCreater.magicCityDis = nodeData.magicCityDis;
        nodeCreater.isRound = nodeData.isRound;
        nodeCreater.lonelyDec = nodeData.lonelyDec;
        nodeCreater.maxAdj = nodeData.maxAdj;
        nodeCreater.maxAdjNode = nodeData.maxAdjNode;
        nodeCreater.minAdj = nodeData.minAdj;
        nodeCreater.minAdjNode = nodeData.minAdjNode;
        nodeCreater.NodeHeight = nodeData.NodeHeight;
        nodeCreater.NodeWidth = nodeData.NodeWidth;
        nodeCreater.nodeRange = nodeData.nodeRange;
        nodeCreater.NodeX = nodeData.NodeX;
        nodeCreater.NodeY = nodeData.NodeY;
    }
    public void SaveEventData(List<MapNode> nodes)
    {
        if(eventData == null)
        {
            eventData = new MapEventData()
            {
                eventId = new List<int>(),
                eventDay = new List<int>()
            };
        }
        eventData.eventId.Clear();
        eventData.eventDay.Clear();
        foreach(var node in nodes)
        {
            eventData.eventId.Add(node.GetComponent<NodeEvent>().EventId);
            eventData.eventDay.Add(node.GetComponent<NodeEvent>().Day);
        }
        GameSave.SaveByJson("EventData.json", eventData);
    }
    public void LoadEventData(List<MapNode> nodes)
    {
        if(eventData == null)
        {
            eventData = new MapEventData()
            {
                eventId = new List<int>(),
                eventDay = new List<int>()
            };
            foreach (var node in nodes)
            {
                var nodeEvent = node.AddComponent<NodeEvent>();
                nodeEvent.EventId = 3003;
                nodeEvent.Day = 999;
            }
        }
        else
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var nodeEvent = nodes[i].AddComponent<NodeEvent>();
                nodeEvent.EventId = eventData.eventId[i];
                nodeEvent.Day = eventData.eventDay[i];
            }
        }

    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
