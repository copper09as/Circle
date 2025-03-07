using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/NewCard")]
public class Card : TScriptableObject
{     
    public string CardName;
    public string CardType;
    public string CardDescription;
    public string CardNumber;
    public Sprite CardSprite;
}
