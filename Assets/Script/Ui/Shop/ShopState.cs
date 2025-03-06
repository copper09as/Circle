using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopState
{
    public abstract ShopUiState state { get; }
    public virtual void Enter()
    {
        Shop.Instance.currentState = this;
    }
    public virtual void Exit()
    {
        Shop.Instance.currentState = null;
    }
}
public enum ShopUiState
{
    Buy,
    Sold,
    Main
}