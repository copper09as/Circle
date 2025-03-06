using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEventUi : MonoBehaviour
{
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventDescription;
    [SerializeField] private Button SkipButton;
    private void Start()
    {
        SkipButton.onClick.AddListener(Exit);
    }
    private void Exit()
    {
        Destroy(gameObject);
    }
}
