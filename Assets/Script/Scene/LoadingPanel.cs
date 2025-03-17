using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
    public class LoadingPanel : BasePopUI
    {

        [SerializeField] private SliderBar sliderBar;
        [SerializeField] private TMP_Text tipText;

        [Header("提示文本")]
        public List<string> tips;
        private int curTipIndex = 0;

        [Header("更改提示文本的间隔")]
        public float changeTime = 0.8f;
        private float changeTipTime = 0.8f;
        public bool HideInEnterScene = true;

        private void Awake()
        {
        //gameObject.SetActive(false);
        if (HideInEnterScene)
            {
                Hide();
            }
        
        }

        public void Update()
        {
            if (Visible)
            {
                changeTipTime -= Time.deltaTime;
                if (changeTipTime <= 0)
                {
                    changeTipTime = changeTime;
                    // 更改当前的提示文本
                    curTipIndex++;
                    if (curTipIndex >= tips.Count)
                    {
                        curTipIndex = 0;
                    }
                    tipText.text = tips[curTipIndex];
                }
            }
        }

        public void SetLoadingProcess(float maxProcess, float curProcess)
        {
            sliderBar.UpdateSliderBar(maxProcess, curProcess, true);
        }

    }
