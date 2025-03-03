using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour,IPointerDownHandler
{
    public ItemData itemData;
    public int Mount;
    public TextMeshProUGUI mountText;
    [SerializeField] private Shop shop;

    public void OnPointerDown(PointerEventData eventData)
    {
        shop.RemoveItem(itemData.id, Mount);
    }
    internal void ClearData()
    {
        Mount = 0;
        itemData = null;
        GetComponent<Image>().sprite = null;
        mountText.text = Mount.ToString();
    }
}
