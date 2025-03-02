using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : SingleTon<UiManager>
{
    [SerializeField] private TextMeshProUGUI dayText;
    private void OnEnable()
    {
        EventManager.updateMapUi += UpdateText;
    }
    private void OnDisable()
    {
        EventManager.updateMapUi -= UpdateText;
    }
    private void UpdateText()
    {
        dayText.text = StaticResource.day.ToString();
    }

}
