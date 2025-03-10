using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Npc;
using UnityEngine.AddressableAssets;
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
        LoadNpc();
    }
    private void LoadNpc()
    {
        Addressables.InstantiateAsync(eventData.NpcTag).Completed += handle =>
        {
            handle.Result.transform.SetParent(transform, false);
            npc = handle.Result.GetComponent<Npc.Npc>();
            MapEventManager.Instance.npc = npc;
        };
    }
}
