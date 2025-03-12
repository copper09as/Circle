using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeopleProSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI peopleName;
    public void UpdatePro(Sprite sprite,string name)
    {
        image.sprite = sprite;
        peopleName.text = name;
    }
}
