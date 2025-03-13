using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInn : BuildingInNode
{
    public override void Enter()
    {
        MainGame.State.Instance.currentState = MainGame.GameState.Inn;
        StartCoroutine(SceneChangeManager.Instance.LoadScene("Inn", 1));
        Debug.Log("½øÈëÁËÂÃ¹İ");
    }


}
