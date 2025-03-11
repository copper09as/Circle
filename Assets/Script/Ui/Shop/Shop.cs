using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : SingleTon<Shop>
{
    [SerializeField] private Button Leave;
    public List<ShopSlot> slots;
    public List<itemId> items;
    public ItemData selectItem;
    public Canvas canvas;
    public ShopState currentState;//处理当前状态
    public Bag bag;
    private void Awake()
    {
        if (bag == null)
            bag = GameObject.Find("Bag").GetComponent<Bag>();
        Leave.onClick.AddListener(LeaveShop);

    }
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
            slots[i].GetComponent<Image>().sprite = item.image;
            slots[i].mountText.text = slots[i].Mount.ToString();
        }
        EventManager.UpdateMapUi();
    }
    private void LeaveShop()
    {
        MainGame.State.Instance.currentState = MainGame.GameState.Map;
        StartCoroutine(SceneChangeManager.Instance.LeaveScene("Shop"));
    }
    public bool BuyItem(int itemMount)
    {

        int id = selectItem.id;
        var item = items.Find(i => i.id == id);
        int remainingItem = item.mount - itemMount;
        if (remainingItem >= 0)
        {
            if (!CanBuy(id, itemMount)) return false;
            InventoryManager.Instance.Gold -= InventoryManager.Instance.FindItem(id).price * itemMount;
            InventoryManager.Instance.AddItem(id, itemMount);
            if (item.mount - itemMount > 0)
                item.mount -= itemMount;
            else
            {
                items.Remove(item);
                slots[items.Count].ClearData();
            }
            EventManager.UpdateSlotUi();

            return true;
        }
        return false;
    }
    private bool CanBuy(int id, int itemMount)
    {
        return (InventoryManager.Instance.Gold - InventoryManager.Instance.FindItem(id).price * itemMount >= 0);
    }
    public void SoldItem(int mount)
    {
        int id = selectItem.id;
        var item = bag.items.Find(i => i.id == id);
        if (!CanSold(item.mount, mount)) return;
        InventoryManager.Instance.Gold += InventoryManager.Instance.FindItem(id).price * mount;
        bag.RemoveItem(id, mount);
        EventManager.UpdateSlotUi();
    }
    private bool CanSold(int mount, int soldMount)
    {
        return mount - soldMount >= 0;
    }
}

