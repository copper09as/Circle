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

    [Header("���ƴ�������")]
    public List<CardDisplay> Deck = new List<CardDisplay>();
    public List<CardDisplay> Hand = new List<CardDisplay>();

    private async void Awake()
    {
        canvas = GameObject.Find("Canvas");
        handCard = new GameObject
        {
            name = "HandCard"
        };
        handCard.transform.parent = canvas.transform;
        pool = new CardPool();
        //pool.Create();

        for (int i = 0; i < 10; i++)
        {
            var a = Random.Range(0, 2);
            if (a == 0) await GainMagic.GainCard("Heal", handCard);
            else await GainMagic.GainCard("Ruin", handCard);
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
        Event.DrawCard(MagicNumber.initDrawNumber);//�鿨

    }*/
    public void CardSort()
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
            Debug.LogWarning("�������");
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
