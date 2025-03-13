using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GainMagic 
{
    public static async Task<Magic> Gain(string name)
    {
        var handle =Addressables.LoadAssetAsync<Magic>(name);
        await handle.Task; 
        return handle.Result;
    }
}
