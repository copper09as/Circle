using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    [SerializeField]bool candrag;
    [SerializeField]Vector2 mouse;
    [SerializeField]Vector3 Pos;
    RectTransform rectTransform;
    GameObject Gameobject;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.CompareTag("MeditationMap"))
        { candrag = true; Gameobject = eventData.pointerEnter; }
        else candrag = false;
        mouse = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter.CompareTag("MeditationMap"))
            candrag = true;
        else candrag = false;
        if (!candrag) return;
        eventData.pointerEnter.transform.position = eventData.pointerEnter.transform.position - (Vector3)(mouse - (Vector2)Input.mousePosition);
        mouse = Input.mousePosition;
        Pos = eventData.pointerEnter.transform.position;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        candrag = false;
    }

    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = Vector3.zero;
    }
    

    public void OnScroll(PointerEventData eventData)
    {
        Zooming();
    }
    void Zooming()
    {
        if (!candrag) return;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Gameobject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log(Gameobject.transform.localScale);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            Gameobject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

}
