using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : SingleTon<UiManager>
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private Button turnEnd;
    public List<Sprite> NumberSprites;
    private void OnEnable()
    {
        EventManager.updateMapUi += UpdateText;
        turnEnd.onClick.AddListener(EventManager.NextDay);
    }
    private void OnDisable()
    {
        EventManager.updateMapUi -= UpdateText;
    }
    private void UpdateText()
    {
        dayText.text = GameDataManager.Instance.day.ToString();
        moveText.text = GameDataManager.Instance.move.ToString();
    }

}
