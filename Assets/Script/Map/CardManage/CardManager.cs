using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class CardManager : SingleTon<CardManager>
{
    private GameObject canvas;
    public CardPool pool;
    public GameObject handCard;

    [Header("ø®≈∆¥¢¥Ê«¯”Ú")]
    public List<CardDisplay> Deck = new List<CardDisplay>();
    public List<CardDisplay> Hand = new List<CardDisplay>();

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        handCard = new GameObject
        {
            name = "HandCard"
        };
        handCard.transform.parent = canvas.transform;
        pool = new CardPool();
        //pool.Create();
        foreach(var card in Deck)
        {
            var insCard = Instantiate(card, handCard.transform);
            Hand.Add(insCard);
        }
        CardSort();

    }

    /*private void OnEnable()
    {
        Event.drawCard += DrawCard;
        Event.handSort += CardSort;
        Event.clearCard += ClearHand;
        Event.displayCard += Display;
        Event.turnRefresh += DrawCard;
    }
    private void OnDisable()
    {
        Event.drawCard -= DrawCard;
        Event.handSort -= CardSort;
        Event.clearCard -= ClearHand;
        Event.displayCard -= Display;
        Event.turnRefresh -= DrawCard;
    }*/

    /*private void Start()
    {
        
        deckOperate.ShuffleDeck(tempCard);
        Event.DrawCard(MagicNumber.initDrawNumber);//≥Èø®

    }*/
    private void CardSort()
    {
        for(int i = 0;i< Hand.Count;i++)
        {
            CardDisplay card = Hand[i];
            card.transform.SetAsLastSibling();
            float spacing = Mathf.Max(50f, 1200f / (Hand.Count + 1));
            float xPosition = 400 + i * spacing;
            card.transform.position = new Vector2(xPosition, 185);
            Debug.Log(i);
        }
    }
   private  void ClearHand()
    {
        while(Hand.Count > 0)
        {
            Debug.LogWarning("«Â≥˝ ÷≈∆");
            //pool.CardMove(Hand[0], DiscardPile, Hand,true);
        }
    }
    private void Display(List<CardDisplay> disCard,bool isShow)
    {
        foreach(var card in disCard)
        {
            card.gameObject.SetActive(isShow);
        }
    }

}
