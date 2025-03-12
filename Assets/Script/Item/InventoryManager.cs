using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingleTon<InventoryManager>,IDestroySelf
{
    public BagSaveData saveData;
    public List<itemId> items;//���ӵ�е���Ʒ��ʹ��id����������
    public List<List<itemId>> npcItems;
    public List<ItemData> itemData;
    [SerializeField] private ItemSoList npcWareHouse;
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
            titleUi.importantManager.Add(this);
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

    public ItemData FindItem(int id)//����id������Ʒ����
    {
        return itemData.Find(i => i.id == id);
    }
    public void RemoveItem(int id, int itemMount)//�Ƴ���Ʒ
    {
        bool isBeyond = false;
        if(items.Find(i=>i.id == id)!=null)
        {
            isBeyond = true;
        }

        if (isBeyond)
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
    public void AddItem(int id, int itemMount)//�����Ʒ
    {
        bool isBeyond = false;
        if (items.Find(i => i.id == id) != null)
        {
            isBeyond = true;
        }
        if (isBeyond)
        {
            items.Find(i => i.id == id).mount += itemMount;
        }

        else
        {
            var _item = new itemId
            {
                id = id,
                mount = itemMount
            };
            items.Add(_item);
        }
        EventManager.UpdateSlotUi();
    }
    private void SaveBagData()
    {
        GameSave.SaveByJson("BagData.json", saveData);
        Debug.Log("���Դ浵����");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
