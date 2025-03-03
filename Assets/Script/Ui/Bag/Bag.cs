using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public List<Slot> slots;
    public List<itemId> items;
    public itemId il;
    private void OnEnable()
    {
        EventManager.updateSlotUi += UpdateSlotUi;
    }
    private void OnDisable()
    {
        EventManager.updateSlotUi -= UpdateSlotUi;
    }
    void Start()
    {
        items = InventoryManager.Instance.items;

        EventManager.UpdateSlotUi();
    }
    public void RemoveItem(int id, int itemMount)
    {
        EventManager.RemoveItem(id, itemMount);
        slots[items.Count].ClearData();
        EventManager.UpdateSlotUi();

    }
    public void AddItem(int id, int itemMount)
    {
        EventManager.AddItem(id, itemMount);
        EventManager.UpdateSlotUi();
    }
    private void UpdateSlotUi()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = InventoryManager.Instance.FindItem(items[i].id);
            slots[i].Mount = items[i].mount;
            slots[i].itemData = item;
            slots[i].GetComponent<Image>().sprite = item.sprite;
            slots[i].mountText.text = slots[i].Mount.ToString();        
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddItem(il.id,1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemoveItem(il.id, 1);
        }
    }
}
