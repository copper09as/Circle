using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUi : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    [SerializeField] Button BuyButton;
    [SerializeField] Button CancleButton;
    public Image itemImage;
    private void Start()
    {
        BuyButton.onClick.AddListener(OnBuy);
        CancleButton.onClick.AddListener(OnCancle);
    }
    private void OnBuy()
    {

        if (!Shop.Instance.BuyItem(1)) return;
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
}
