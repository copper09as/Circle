using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Npc;
using UnityEngine.AddressableAssets;
using MainGame;
public class MapEventUi : MonoBehaviour
{
    public EventData eventData; 
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventDescription;
    public TextMeshProUGUI EventPro;//事件数值显示
    public Npc.Npc npc;
    [SerializeField] private Button SkipButton;
    private void OnEnable()
    {
        State.Instance.currentState = GameState.Event;
        EventManager.eventOver += Exit;
    }
    private void OnDisable()
    {
        State.Instance.currentState = GameState.Map;
        EventManager.eventOver -= Exit;
    }
    private void Start()
    {
        SkipButton.onClick.AddListener(Skip);
    }
    private void Skip()
    {
        MapEventManager.Instance.SkipEffect();
        Exit();
    }
    private void Exit()
    {
        gameObject.SetActive(false);
    }
    public void InitEventUi()
    {
        EventName.text = eventData.Name;
        EventDescription.text = eventData.Description;
    }

}
