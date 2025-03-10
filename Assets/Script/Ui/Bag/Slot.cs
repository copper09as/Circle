using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerDownHandler
{
    public ItemData itemData;
    public int Mount;
    public TextMeshProUGUI mountText;



    internal void ClearData()
    {
        Mount = 0;
        itemData = null;
        GetComponent<Image>().sprite = null;
        mountText.text = Mount.ToString();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemData.id == 0) return;
        if (Shop.Instance.currentState.state != ShopUiState.Main) return;
        Shop.Instance.selectItem = itemData;
        Shop.Instance.TransShopState(new SoldState());
    }
}
