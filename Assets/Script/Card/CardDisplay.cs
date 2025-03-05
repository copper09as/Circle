using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]private int id;
    public itemId itemId;
    [SerializeField] private string cardName;
    [SerializeField] private string cardType;
    [SerializeField] private Image image;


    void Start()
    {
        Refresh();
    }
    public void SetID(int id)=> this.id = id; 
    public void Refresh()
    {
        Addressables.LoadAssetAsync<ItemListSo>("InventoryInventoryList_SO").Completed += 
            (handle)=>
        {
            ItemData data = handle.Result.itemData[id];
            //cardName = data.Name;
            //image.sprite = data.sprite;
        };
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
