using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class SliderUi : MonoBehaviour
{
    private Slider slider;
    private int MaxValue;
    private float perValue;
    public int number;
    private int price;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button AddNumber;
    [SerializeField] private Button DeclineNumber;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SliderChange);
        AddNumber.onClick.AddListener(Add);
        DeclineNumber.onClick.AddListener(Decline);
    }
    public void Init(int value,int price)
    {
        MaxValue = value;
        perValue = 1f / MaxValue;
        number = 0;
        sliderText.text = number.ToString();
        this.price = price;
        priceText.text = "0";
        //sliderText.text = value.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Decline();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Add();
        }
    }
    private void SliderChange(float value)
    {
        number = Mathf.RoundToInt(value / perValue);
        sliderText.text = number.ToString();
        priceText.text = (price * number).ToString();
    }
    private void Add()
    {
        slider.value += perValue;
    }
    private void Decline()
    {
        slider.value -= perValue;
    }
}
