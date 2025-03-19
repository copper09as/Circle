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
    [SerializeField] private Button CloseButton;
    private void OnEnable()
    {
        State.Instance.currentState = GameState.Event;
        EventManager.eventOver += EventClose;
        CloseButton.onClick.AddListener(Skip);
        CloseButton.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CloseButton.onClick.RemoveListener(Skip);
        State.Instance.currentState = GameState.Map;
        EventManager.eventOver -= EventClose;
    }
    private void Start()
    {
        //SkipButton.onClick.AddListener(Skip);
    }
    private void Skip()
    {
        MapEventManager.Instance.SkipEffect();
        Exit();
    }
    private void EventClose()
    {
        CloseButton.gameObject.SetActive(true);
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
