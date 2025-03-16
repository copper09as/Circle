using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>,IDestroySelf
{
    public BagSaveData saveData;
    public List<itemId> items;//玩家拥有的物品，使用id和数量保存
    public List<List<itemId>> npcItems;
    public List<ItemData> itemData;
    [SerializeField] private NpcItemList npcWareHouse;//未来会弃用
    [SerializeField] private TitleUi titleUi;
    public  int Gold
    {
        get
        {
            return saveData.Gold;
        }
        set
        {
            saveData.Gold = value;
        }
    }
    private void Awake()
    {
        if (titleUi != null)
            titleUi.importantManager.Add(this);//用于在删除存档中重置数据
        if (GameSave.LoadByJson<BagSaveData>("BagData.json")!=null)
        {
            saveData = GameSave.LoadByJson<BagSaveData>("BagData.json");
            items = saveData.items;
        }
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        EventManager.updateSlotUi += SaveBagData;
        EventManager.saveGameData += SaveBagData;
        items = saveData.items;
        Gold = saveData.Gold;
    }

    public ItemData FindItem(int id)//根据id返回物品数据
    {
        return itemData.Find(i => i.id == id);
    }
    private bool CanFind(int id)
    {
        return items.Find(i => i.id == id) != null;
    }
    public void RemoveItem(int id, int itemMount)//移除物品
    {
        if (CanFind(id))
        {
            if (items.Find(i => i.id == id).mount - itemMount > 0)
            {
                items.Find(i => i.id == id).mount -= itemMount;
            }
            else if (items.Find(i => i.id == id).mount - itemMount == 0)
            {
                items.Remove(items.Find(i => i.id == id));
                //slots[items.Count].ClearData();
            }
            else
                return;
            EventManager.UpdateSlotUi();
        }
        else
        {
            return;
        }

    }
    public void AddItem(int id, int itemMount)//添加物品
    {
        if (CanFind(id))
        {
            items.Find(i => i.id == id).mount += itemMount;
        }
        else
        {
            items.Add(CreateItem(id,itemMount));
        }
        EventManager.UpdateSlotUi();
    }
    private itemId CreateItem(int id, int itemMount)//新建物品
    {
        Debug.Assert(FindItem(id) != null, "物品库中无此物品，新建无效");
        var _item = new itemId
        {
            id = id,
            mount = itemMount
        };
        return _item;
    }
    private void SaveBagData()
    {
        GameSave.SaveByJson("BagData.json", saveData);
        Debug.Log("测试存档功能");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
