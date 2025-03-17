using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string cardName;
    [SerializeField] private MagicKind cardType;
    [SerializeField] private TMP_Text Text;
    Vector2 initPosition;
    bool isDrag = false;
    bool canDrag = true;
    //[SerializeField] private Image image;
    public Magic magic;

    void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        if (magic == null)
            magic = new Ruin();
        cardName = magic.MagicName;
        cardType = magic.MagicType;
        Text.text = cardName;
    }
    private void Update()
    {
        if (isDrag && canDrag)
            transform.position = Input.mousePosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canDrag) return;
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDrag)
            transform.localScale = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canDrag) return;
        isDrag = true;
        initPosition = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canDrag) return;
        isDrag = false;
        PointerEventData SaveMousePosition = new PointerEventData(EventSystem.current);
        SaveMousePosition.position = Input.mousePosition;
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(SaveMousePosition, result);
        foreach (var ray in result)
        {
            if (ray.gameObject.tag == "CardTrigArea")
            {
                magic.Fuction(MapEventManager.Instance.npc);
                Debug.Log("卡牌触发");
                CardManager.Instance.pool.CardMove(this);
                return;
            }
        }
        CardMove(transform.position, initPosition, 0.5f);
    }
    public void CardMove(Vector2 from, Vector2 to, float duration)
    {
        canDrag = false;
        StartCoroutine(MoveCard(from, to, duration));
    }

    // 协程实现移动逻辑
    private IEnumerator MoveCard(Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        Vector2 startPosition = transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector2.Lerp(from, to, t);

            yield return null;

            elapsed += Time.deltaTime;
        }

        transform.position = to;
        canDrag = true;
    }

    internal void DestroySelf()
    {
        Destroy(gameObject);
    }
}