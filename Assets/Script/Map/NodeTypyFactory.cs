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
        switch(nodeStyle)
        {
            case NodeStyle.City:
                mapNode.AddComponent<BuildingCity>();break;
            case NodeStyle.Shop:
                mapNode.AddComponent<BuildingShop>(); break;
            case NodeStyle.Shrine:
                mapNode.AddComponent<BuildingShrine>(); break;
            case NodeStyle.Inn:
                mapNode.AddComponent<BuildingInn>(); break;
        }
    }

}
