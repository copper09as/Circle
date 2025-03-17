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
        Addressables.InstantiateAsync("Buy").Completed += handle =>
        {
            handle.Result.transform.SetParent(shop.canvas.transform, false);
            handle.Result.GetComponent<BuyUi>().priceText.text =((int)((shop.selectItem.price)*shop.itemDiscount)).ToString();
            //handle.Result.GetComponent<BuyUi>().itemImage.sprite = shop.selectItem.sprite;
        };
        
    }

    public override void Exit()
    {
        base.Exit();
        return;
    }


}
