using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    private Button placeButton;
    public bool CanGet = true;
    public MapNode Left;
    public MapNode Right;
    public MapNode Mid;
    public MapNode Top;
    public List<MapNode> adjancentNode = new List<MapNode>();
    private void Awake()
    {
        float ran = Random.Range(0f, 1f);
        Debug.Log(ran.ToString());
        placeButton = GetComponent<Button>();
        placeButton.onClick.AddListener(TransPlace);
        InitNodeTree();
    }
    private void TransPlace()
    {
        MapNode currentNode = MapManager.Instance.currentNode;
        if (currentNode == null || currentNode == this)
            return;
        if (!MapManager.Instance.CanReach(this))
            return;
        MapManager.Instance.currentNode = this;
        StaticResource.day += 1;
        EventManager.UpdateUi();
    }
    private void InitNodeTree()
    {
        adjancentNode.Clear();
        if (Left != null) adjancentNode.Add(Left);
        if (Right != null) adjancentNode.Add(Right);
        if (Mid != null) adjancentNode.Add(Mid);
        if (Top != null) adjancentNode.Add(Top);
    }
    private void Enter()
    {

    }
    private void Exit()
    {
        
    }
}

