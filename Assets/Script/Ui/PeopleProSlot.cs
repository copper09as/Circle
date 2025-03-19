using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Npc;
using UnityEngine.EventSystems;
public class PeopleProSlot : MonoBehaviour,IPointerDownHandler

{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI peopleName;
    public Npc.Npc npc;
    public void UpdatePro(Npc.Npc npc)
    {
        this.npc = npc;
        peopleName.text = npc.npcName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(npc.npcName);
    }
}
