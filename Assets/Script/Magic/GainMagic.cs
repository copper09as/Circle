using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GainMagic : MonoBehaviour
{
    public static void Gain(string name)
    {
        Addressables.LoadAssetAsync<Magic>(name).Completed += (handle)
        =>
        {
            
        };
    }
}
