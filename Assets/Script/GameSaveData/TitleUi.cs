using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitleUi : MonoBehaviour
{
    [SerializeField] private Button delSave;
    [SerializeField] private List<IDestroySelf> importantManager;
    private void OnEnable()
    {
        delSave.onClick.AddListener(DelSave);
    }
    private void DelSave()
    {
        GameSave.DeleteFile("DayData.json");
        GameSave.DeleteFile("NodeData.json");
        GameSave.DeleteFile("BagData.json");
        Debug.Log("ÕýÔÚÉ¾³ý´æµµ");
        foreach(var i in importantManager)
        {
            i.DestroySelf();
        }
        StartCoroutine(SceneChangeManager.Instance.LoadSceen("Resident", 0));

    }
}
