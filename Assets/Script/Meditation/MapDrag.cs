using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]bool candrag;
    [SerializeField]Vector2 mouse;
    [SerializeField]Vector3 Pos;
    RectTransform rectTransform;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter.CompareTag("MeditationMap"))
            candrag=true;
        else candrag = false;
        mouse = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.CompareTag("MeditationMap"))
            candrag = true;
        else candrag = false;
        if (!candrag) return;
        eventData.pointerEnter.transform.position = eventData.pointerEnter.transform.position - (Vector3)(mouse- (Vector2)Input.mousePosition);
        mouse = Input.mousePosition;
        Pos = eventData.pointerEnter.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        candrag = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
