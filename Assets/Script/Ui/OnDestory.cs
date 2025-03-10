using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDestory : MonoBehaviour
{
    [SerializeField] private Button delButton;
    private void OnEnable()
    {
        delButton.onClick.AddListener(DestorySelf);
    }
    private void OnDisable()
    {
        delButton.onClick.RemoveListener(DestorySelf);
    }
    private void DestorySelf()
    {
        Destroy(gameObject);
    }
}
