using System;
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
    public Dictionary<int, float> discounts;
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
            int itemId = UnityEngine.Random.Range(1, 6);
            var item = InventoryManager.Instance.FindItem(itemId);
            //slots[i].Mount = items[i].mount;
            slots[i].itemData = item;
            slots[i].GetComponent<Image>().sprite = item.image;
            //slots[i].mountText.text = slots[i].Mount.ToString();
        }
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemData != null && !discounts.ContainsKey(slots[i].itemData.id))
                discounts.Add(slots[i].itemData.id, UnityEngine.Random.Range(0.4f, 2.0f));
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
        int price = GetPrice(id, itemMount, GetDiscount, 1f);
        /*var item = items.Find(i => i.id == id);
        //int remainingItem = item.mount - itemMount;
        //if (remainingItem >= 0)
        //{*/
        if (!CanBuy(id, itemMount, price)) return false;
        InventoryManager.Instance.Gold -= price;
        InventoryManager.Instance.AddItem(id, itemMount);
        /*if (item.mount - itemMount > 0)
        //item.mount -= itemMount;
        //else
        //{
        //items.Remove(item);
        //slots[items.Count].ClearData();
        //}*/
        EventManager.UpdateSlotUi();
        EventManager.UpdateMapUi();
        return true;
        //}
        //return false;
    }

    public void SoldItem(int mount)
    {
        int id = selectItem.id;
        var item = bag.items.Find(i => i.id == id);
        if (!CanSold(item.mount, mount)) return;
        InventoryManager.Instance.Gold += GetPrice(id, mount, GetDiscount, 0.8f);
        bag.RemoveItem(id, mount);
        EventManager.UpdateSlotUi();
        EventManager.UpdateMapUi();
    }
    public int GetPrice(int id, int itemMount, Func<int, Dictionary<int, float>, float> GetDiscount, float initDiscount)
    {
        return (int)(initDiscount * InventoryManager.Instance.FindItem(id).price * GetDiscount(id, discounts)) * itemMount;
    }
    private Func<int,int,int,bool> CanBuy
        = (id, itemMount,price)
        => itemMount != 0 && ((InventoryManager.Instance.Gold - price)*itemMount >= 0);

    public Func<int, Dictionary<int, float>, float> GetDiscount 
        = (id, dic) 
        => dic.ContainsKey(id) ? 1f : dic[id];

    private Func<int, int,bool> CanSold
        = (mount,soldMount) 
        => soldMount != 0&&(mount - soldMount >= 0);
}

