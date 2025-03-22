using UnityEngine.AddressableAssets;

public class BuyState : ShopState
{
    public override ShopUiState state
    {
        get { return ShopUiState.Buy; }
    }

    public override void Enter()//进入后生成购买界面
    {
        base.Enter();
        var shop = Shop.Instance;
        int price = (int)(shop.selectItem.price * shop.GetDiscount(shop.selectItem.id,shop.discounts));
        Addressables.InstantiateAsync("Buy").Completed += handle =>
        {
            handle.Result.transform.SetParent(shop.canvas.transform, false);
            //handle.Result.GetComponent<BuyUi>().priceText.text =price.ToString();
            int number = handle.Result.GetComponent<BuyUi>().MaxBuy(price);
            //handle.Result.GetComponent<BuyUi>().itemImage.sprite = shop.selectItem.sprite;
        };
        
    }
    public override void Exit()
    {
        base.Exit();
        return;
    }


}
