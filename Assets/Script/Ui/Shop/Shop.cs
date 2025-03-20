using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : SingleTon<Shop>
{
    [SerializeField] private Button Leave;
    public List<ShopSlot> slots;
    //public List<itemId> items;
    public ItemData selectItem;
    public Canvas canvas;
    public ShopState currentState;//处理当前状态
    public Bag bag;
    public float itemDiscot;
    private Dictionary<int, float> discounts;
    private void Awake()
    {
        discounts = new Dictionary<int, float>();

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
        
        for (int i = 0; i < slots.Count; i++)
        {
            int itemId = Random.Range(1, 6);
            var item = InventoryManager.Instance.FindItem(itemId);
            //slots[i].Mount = items[i].mount;
            slots[i].itemData = item;
            slots[i].GetComponent<Image>().sprite = item.image;
            //slots[i].mountText.text = slots[i].Mount.ToString();
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemData != null && !discounts.ContainsKey(slots[i].itemData.id))
                discounts.Add(slots[i].itemData.id, Random.Range(0.4f, 2.0f));
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
        //注释部分为减少商店物品数量的方法
        int id = selectItem.id;
        //var item = items.Find(i => i.id == id);
        //int remainingItem = item.mount - itemMount;
        //if (remainingItem >= 0)
        //{
            if (!CanBuy(id, itemMount)) return false;
            InventoryManager.Instance.Gold -= (int)(InventoryManager.Instance.FindItem(id).price * itemMount * GetDiscount(id));
            InventoryManager.Instance.AddItem(id, itemMount);
        //if (item.mount - itemMount > 0)
        //item.mount -= itemMount;
        //else
        //{
        //items.Remove(item);
        //slots[items.Count].ClearData();
        //}
        EventManager.UpdateSlotUi();
        EventManager.UpdateMapUi();
        return true;
        //}
        //return false;
    }
    private bool CanBuy(int id, int itemMount)
    {
        return (InventoryManager.Instance.Gold - InventoryManager.Instance.FindItem(id).price * itemMount * GetDiscount(id) >= 0);
    }
    public void SoldItem(int mount)
    {
        int id = selectItem.id;
        var item = bag.items.Find(i => i.id == id);
        if (!CanSold(item.mount, mount)) return;
        InventoryManager.Instance.Gold += (int)(0.8f*InventoryManager.Instance.FindItem(id).price * mount * GetDiscount(id));
        bag.RemoveItem(id, mount);
        EventManager.UpdateSlotUi();
        EventManager.UpdateMapUi();
    }
    public float GetDiscount(int id)
    {
        if (!discounts.ContainsKey(id))
            return 1f;
        return discounts[id];
    }

    private bool CanSold(int mount, int soldMount)
    {
        return mount - soldMount >= 0;
    }
}

