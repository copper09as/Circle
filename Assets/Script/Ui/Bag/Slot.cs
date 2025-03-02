using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData itemData;
    public int Mount;
    [SerializeField]private TextMeshProUGUI mountText;
    private void OnEnable()
    {
        EventManager.updateSlotUi += RefreshSlot;
    }
    private void OnDisable()
    {
        EventManager.updateSlotUi -= RefreshSlot;
    }
    internal void RefreshSlot()
    {
        GetComponent<Image>().sprite = itemData.sprite;
        mountText.text = Mount.ToString();
    }
    private void Start()
    {
        EventManager.UpdateSlotUi();
    }
}
