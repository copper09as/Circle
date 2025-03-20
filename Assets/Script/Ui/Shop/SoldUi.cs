using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldUi : MonoBehaviour
{
    //public TextMeshProUGUI priceText;
    [SerializeField] Button SoldButton;
    [SerializeField] Button CancleButton;
    [SerializeField] SliderUi slider;
    public Image itemImage;
    private void Start()
    {
        SoldButton.onClick.AddListener(OnSold);
        CancleButton.onClick.AddListener(OnCancle);
    }
    private void OnSold()
    {
        Shop.Instance.SoldItem(slider.number);
        Shop.Instance.selectItem = null;
        Shop.Instance.TransShopState(new ShopNormalState());
        Destroy(gameObject);
    }
    private void OnCancle()
    {
        Shop.Instance.selectItem = null;
        Shop.Instance.TransShopState(new ShopNormalState());
        Destroy(gameObject);
    }
    public int MaxSold(int price)
    {
        int number = Shop.Instance.bag.items.Find(i => i.id == Shop.Instance.selectItem.id).mount;
        slider.Init(number,price);
        //priceText.text = (price * number).ToString();
        return number;
    }
}
