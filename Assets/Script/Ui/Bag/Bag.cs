using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using MainGame;
public class Bag : MonoBehaviour
{
    [SerializeField] private Slot slotPrefab;
    public List<Slot> slots;
    public List<itemId> items;
    [SerializeField] private int SlotCount;
    private void OnEnable()
    {

        EventManager.updateSlotUi += UpdateSlotUi;
        
    }
    private void OnDisable()
    {
        State.Instance.currentState = GameState.Map;
        EventManager.updateSlotUi -= UpdateSlotUi;
        
    }
    void Start()
    {
        items = InventoryManager.Instance.items;
        UpdateSlotUi();
    }
    public void RemoveItem(int id, int itemMount)
    {
        InventoryManager.Instance.RemoveItem(id, itemMount);
        items = InventoryManager.Instance.items; // 刷新列表

        if (items.Count < slots.Count)
        {
            slots[items.Count].ClearData();
        }

        EventManager.UpdateSlotUi();
    }
    public void AddItem(int id, int itemMount)
    {
        InventoryManager.Instance.AddItem(id, itemMount);
    }
    private void UpdateSlotUi()
    {
        items = InventoryManager.Instance.items;

        // 动态扩展槽位
        while (slots.Count < items.Count || slots.Count < SlotCount)
        {
            var newSlot = Instantiate(slotPrefab, transform);
            slots.Add(newSlot.GetComponent<Slot>());
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (i >= slots.Count) break; // 防止越界

            var item = InventoryManager.Instance.FindItem(items[i].id);
            if (item == null || slots[i] == null) continue; // 空值保护

            slots[i].Mount = items[i].mount;
            slots[i].itemData = item;
            slots[i].GetComponent<Image>().sprite = item.image;
            slots[i].mountText.text = slots[i].Mount.ToString();
        }
    }

}
