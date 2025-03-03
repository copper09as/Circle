using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public ItemData itemData;
    public int Mount;
    public TextMeshProUGUI mountText;
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
//
    }
    private void Start()
    {
        EventManager.UpdateSlotUi();
    }

    internal void ClearData()
    {
        Mount = 0;
        itemData = null;
        GetComponent<Image>().sprite = null;
        mountText.text = Mount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
