using System.Collections;
using System.Collections.Generic;
using MainGame;
using UnityEngine;

public class BuildingShop : BuildingInNode
{
    public HashSet<itemId> shopIds = new HashSet<itemId>();
    public void UpdateShop()
    {
        //���ˢ����Ʒ�Լ���Ʒ�۸�
    }
    public override void Enter()
    {
        if (CanReach == false) return;
        CanReach = false;
        MainGame.State.Instance.currentState = GameState.Shop;
        StartCoroutine(SceneChangeManager.Instance.LoadScene("Shop", 1));
    }
}
