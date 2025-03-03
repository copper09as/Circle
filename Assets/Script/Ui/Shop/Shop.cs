using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : SingleTon<Shop>
{
    public List<ShopSlot> slots;
    public List<itemId> items;
    public ItemData selectItem;
    public Canvas canvas;
    public ShopState currentState;
    private void Start()
    {
        UpdateShopSlotUi();
        currentState = new ShopNormalState();
    }
    public void TransShopState(ShopState EnterState)
    {
        if (currentState != null)
            currentState.Exit();
        EnterState.Enter();
    }
    private void UpdateShopSlotUi()
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
    public bool BuyItem(int itemMount)
    {
        int id = selectItem.id;
        var item = items.Find(i => i.id == id);
        int remainingItem = item.mount - itemMount;
        if (remainingItem >= 0)
        {
            if (!CanBuy(id, itemMount)) return false;
            StaticResource.Gold -= InventoryManager.Instance.FindItem(id).price * itemMount;
            EventManager.AddItem(id, itemMount);
            if(item.mount - itemMount>0)
                item.mount -= itemMount;
            else
            {
                items.Remove(item);
                slots[items.Count].ClearData();
            }
            UpdateShopSlotUi();
            return true;
        }
        return false;
    }
    private bool CanBuy(int id, int itemMount)
    {
        return (StaticResource.Gold - InventoryManager.Instance.FindItem(id).price * itemMount >= 0);
    }
}
