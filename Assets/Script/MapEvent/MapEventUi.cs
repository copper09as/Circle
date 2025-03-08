using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEventUi : MonoBehaviour
{
    public EventData eventData; 
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventDescription;
    public TextMeshProUGUI EventPro;//事件数值显示
    [SerializeField] private Button SkipButton;
    private void OnEnable()
    {
        EventManager.eventOver += Exit;
    }
    private void OnDisable()
    {
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
        Destroy(gameObject);
    }
    public void InitEventUi()
    {
        EventName.text = eventData.Name;
        EventDescription.text = eventData.Description;
        EventPro.text = " Limit"+eventData.Limit;
    }
}
