
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUi : MonoBehaviour
{
    //public TextMeshProUGUI priceText;
    [SerializeField] Button BuyButton;
    [SerializeField] Button CancleButton;
    [SerializeField] SliderUi slider;
    public Image itemImage;
    private void Start()
    {
        BuyButton.onClick.AddListener(OnBuy);
        CancleButton.onClick.AddListener(OnCancle);
    }
    private void OnBuy()
    {
        if (!Shop.Instance.BuyItem(slider.number)) return;
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
    public int MaxBuy(int price)
    {
        int number = InventoryManager.Instance.Gold / price;
        slider.Init(number,price);
        //priceText.text = (price * number).ToString();
        return number;
    }
}
