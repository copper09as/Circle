using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public ItemData itemData;
    public int Mount;
    public TextMeshProUGUI mountText;


    private void Start()
    {
        transform.parent.GetComponent<Bag>().slots.Add(this);
    }

    internal void ClearData()
    {
        Mount = 0;
        itemData = null;
        GetComponent<Image>().sprite = null;
        mountText.text = Mount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (itemData.id == 0) return;
        Shop.Instance.selectItem = itemData;
        Shop.Instance.TransShopState(new SoldState());


    }
}
