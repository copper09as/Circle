using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SoldState : ShopState
{
    public override ShopUiState state
    {
        get { return ShopUiState.Sold; }
    }

    public override void Enter()
    {
        base.Enter();
        var shop = Shop.Instance;
        Addressables.InstantiateAsync("Sold").Completed += handle =>
        {
            handle.Result.transform.SetParent(shop.canvas.transform, false);
            handle.Result.GetComponent<SoldUi>().priceText.text = shop.selectItem.price.ToString();
            //handle.Result.GetComponent<SoldUi>().itemImage.sprite = shop.selectItem.sprite;
        };

    }

    public override void Exit()
    {
        base.Exit();
        return;
    }
}
