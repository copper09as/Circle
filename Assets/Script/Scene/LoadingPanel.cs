using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
    public class LoadingPanel : BasePopUI
    {

        [SerializeField] private SliderBar sliderBar;
        [SerializeField] private TMP_Text tipText;

        [Header("��ʾ�ı�")]
        public List<string> tips;
        private int curTipIndex = 0;

        [Header("������ʾ�ı��ļ��")]
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
                    // ���ĵ�ǰ����ʾ�ı�
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
