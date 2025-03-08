using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : SingleTon<GameDataManager>
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
