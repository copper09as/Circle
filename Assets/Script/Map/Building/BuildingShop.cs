using System.Collections;
using System.Collections.Generic;
using MainGame;
using UnityEngine;

public class BuildingShop : BuildingInNode
{
    public override void Enter()
    {
        if (CanReach == false) return;
        CanReach = false;
        MainGame.State.Instance.currentState = GameState.Shop;
        StartCoroutine(SceneChangeManager.Instance.LoadScene("Shop", 1));
    }
}
