using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour,IPointerDownHandler
{
    public ItemData itemData;
    public int Mount;
    public TextMeshProUGUI mountText;

    public void OnPointerDown(PointerEventData eventData)
    {
        var shop = Shop.Instance;
        if (itemData.id == 0) return;
        if (shop.currentState.state != ShopUiState.Main) return;
        shop.selectItem = itemData;
        shop.TransShopState(new BuyState());
        /*Addressables.InstantiateAsync("Buy").Completed += handle =>
        {
            handle.Result.transform.SetParent(Shop.Instance.canvas.transform, false);
            handle.Result.GetComponent<BuyUi>().priceText.text = itemData.price.ToString();
            handle.Result.GetComponent<BuyUi>().itemImage.sprite = itemData.sprite;
        };*/
    }
    internal void ClearData()
    {
        Mount = 0;
        itemData = null;
        GetComponent<Image>().sprite = null;
        mountText.text = Mount.ToString();
    }
    
}
