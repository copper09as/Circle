using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string cardName;
    [SerializeField] private MagicKind cardType;
    [SerializeField] private TMP_Text Text;
    //[SerializeField] private Image image;
    public Magic magic;

    void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        cardName = magic.MagicName;
        cardType = magic.MagicType;
        Text.text = cardName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
