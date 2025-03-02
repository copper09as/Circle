using System.Collections.Generic;
using UnityEngine;

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
        EventManager.UpdateSlotUi();
    }
    public void AddItem(itemId item)
    {

        try
        {
            items.Find(i => i.id == item.id).mount += item.mount;
        }
            
        catch
        {
            var _item = items.Find(i => i.id == item.id);
            items.Add(_item);
            
        }
        EventManager.UpdateSlotUi();
    }
    private void UpdateSlotUi()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var item = InventoryManager.Instance.FindItem(items[i].id);
            slots[i].Mount = items[i].mount;
            slots[i].itemData = item;
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddItem(il);
        }
        
    }

}
