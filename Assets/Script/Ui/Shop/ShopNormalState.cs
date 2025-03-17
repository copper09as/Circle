using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNormalState : ShopState
{
    public override ShopUiState state
    {
        get { return ShopUiState.Main; }
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
