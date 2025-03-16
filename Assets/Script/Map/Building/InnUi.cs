using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search.Providers;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class InnUi : MonoBehaviour
{
    [SerializeField] private Button use;
    [SerializeField] private Button exit;
    private void OnEnable()
    {
        use.onClick.AddListener(Use);
        exit.onClick.AddListener(Exit);
    }
    private void Use()
    {
        if (InventoryManager.Instance.Gold < 60)
        {
            Debug.Log("Need Gold");
            return;
        }
            
        InventoryManager.Instance.Gold -= 60;
        MapManager.Instance.character.currentHealth += 500;
        EventManager.UpdateMapUi();
    }
    private void Exit()
    {
        MainGame.State.Instance.currentState = MainGame.GameState.Map;
        StartCoroutine(SceneChangeManager.Instance.LeaveScene("Inn"));
    }
}
