using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using MainGame;
public class UiManager : SingleTon<UiManager>
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Button turnEnd;
    [SerializeField] private Button bagButton;
    public List<Sprite> NumberSprites;
    private void OnEnable()
    {
        EventManager.updateMapUi += UpdateText;
        turnEnd.onClick.AddListener(EventManager.NextDay);
        bagButton.onClick.AddListener(OpenBag);
    }
    private void OnDisable()
    {
        EventManager.updateMapUi -= UpdateText;
        turnEnd.onClick?.RemoveListener(EventManager.NextDay);
        bagButton.onClick?.RemoveListener(OpenBag);
    }
    private void UpdateText()
    {
        dayText.text = GameDataManager.Instance.day.ToString();
        moveText.text = GameDataManager.Instance.move.ToString();
        goldText.text = InventoryManager.Instance.Gold.ToString();
    }
    private void OpenBag()
    {
        if (State.Instance.currentState != GameState.Map)
            return;
        State.Instance.currentState = GameState.Bag;
        Addressables.InstantiateAsync("Bag").Completed += handle =>
        {
            var canvas = GameObject.Find("Canvas");
            handle.Result.transform.SetParent(canvas.transform, false);
        };
        
    }

}
