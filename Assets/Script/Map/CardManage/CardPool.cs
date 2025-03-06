using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPool : MonoBehaviour
{

    public List<CardDisplay> Create()
    {
        /*foreach (Card C in CardBeg.Instance.Cards)
        {
            CardCreatManager.CardCreat(CardManager.Instance.handCard, C);
        }*/

        return null;
    }
    public void Push(List<CardDisplay> bePushList, CardDisplay card)
    {
        bePushList.Add(card);
    }
    public CardDisplay Pop(List<CardDisplay> bePopList,bool isDestory)
    {
        Debug.Log(bePopList.Count);
        CardDisplay card = bePopList[bePopList.Count - 1];
        card.gameObject.SetActive(!isDestory);
        bePopList.Remove(card);
        return card;
    }
    public void CardMove(CardDisplay card,List<CardDisplay> enterDomain, List<CardDisplay> reomveDomain,bool isDestory)
    {
        card.gameObject.SetActive(!isDestory);
        enterDomain.Add(card);
        reomveDomain.Remove(card);
    }
    public void CardMove(CardDisplay card, List<CardDisplay> enterDomain, bool isDestory)
    {
        card.gameObject.SetActive(!isDestory);
        enterDomain.Add(card);
    }
}
