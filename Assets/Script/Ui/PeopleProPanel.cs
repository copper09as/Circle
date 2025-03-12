using System.Collections;
using System.Collections.Generic;
using MainGame;
using UnityEngine;
using UnityEngine.UI;

public class PeopleProPanel : MonoBehaviour
{
    [SerializeField] private Button hideButton;
    [SerializeField] private List<PeopleProSlot> slots;
    private void OnEnable()
    {
        hideButton.onClick.AddListener(Hide);
    }
    private void OnDisable()
    {
        hideButton.onClick.RemoveListener(Hide);
    }
    private void Start()
    {
        for(int i = 0;i<MapManager.Instance.currentNode.stayNpc.Count;i++)
        {
            slots[i].UpdatePro(null, MapManager.Instance.currentNode.stayNpc[i].npcName);
        }
    }
    void Hide()
    {
        State.Instance.currentState = GameState.Map;
        gameObject.SetActive(false);
    }

}
