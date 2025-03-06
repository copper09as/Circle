using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldUi : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    [SerializeField] Button SoldButton;
    [SerializeField] Button CancleButton;
    public Image itemImage;
    private void Start()
    {
        SoldButton.onClick.AddListener(OnSold);
        CancleButton.onClick.AddListener(OnCancle);
    }
    private void OnSold()
    {
        Shop.Instance.SoldItem(1);
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
