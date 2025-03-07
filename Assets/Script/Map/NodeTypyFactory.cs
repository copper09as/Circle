using Unity.VisualScripting;

public class NodeTypyFactory
{
    NodeStyle nodeStyle;
    MapNode mapNode;
    public NodeTypyFactory(NodeStyle nodeStyle,MapNode mapNode)
    {
        this.nodeStyle = nodeStyle;
        this.mapNode = mapNode;
    }
    public void AddBuilding()
    {
        MapManager mapManager = MapManager.Instance;
        switch(nodeStyle)
        {
            case NodeStyle.City:
                mapNode.AddComponent<BuildingCity>();
                mapManager.CityNodes.Add(mapNode);
                break;
            case NodeStyle.Shop:
                mapNode.AddComponent<BuildingShop>();
                mapManager.ShopNodes.Add(mapNode); 
                break;
            case NodeStyle.Shrine:
                mapNode.AddComponent<BuildingShrine>();
                mapManager.ShrineNodes.Add(mapNode); 
                break;
            case NodeStyle.Inn:
                mapNode.AddComponent<BuildingInn>(); 
                mapManager.InnNodes.Add(mapNode);
                break;
        }
    }

}
