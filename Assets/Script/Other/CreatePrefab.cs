using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class CreatePrefab : MonoBehaviour
{
    public static void Creat(string name,Transform parent)
    {
        Addressables.InstantiateAsync(name).Completed += handle =>
        {
            handle.Result.transform.SetParent(parent, false);
        };
    }
}
