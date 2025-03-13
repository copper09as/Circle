using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GainMagic:MonoBehaviour 
{
    public static async Task<Magic> GainSO(string name)//获取对应魔法的SO
    {
        var handle =Addressables.LoadAssetAsync<Magic>(name);
        await handle.Task;
        if (handle.IsValid())
            return handle.Result;
        else
        { Debug.Log("未找到魔法" + name); return null; }
    }
    public static async Task<GameObject> LoadCardPrefabAsync(GameObject Parent)//加载卡牌预制体
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Card");
        await handle.Task;
        if (handle.IsValid())
        {
            GameObject card = handle.Result;
            return Instantiate(card, Parent.transform);
            //卡牌预制体实例化到指定父物体下，并返回该预制体
        }
        else
        { Debug.Log("未找到Card预制体"); return null; }
    }
    public static async void GainCard(string magicName, GameObject Parent)//获取卡牌
    {
        GameObject card=await LoadCardPrefabAsync(Parent);
        CardDisplay display = card.GetComponent<CardDisplay>();
        //赋值SO
        display.magic = await GainSO(magicName);

        display.Refresh();
        //对卡牌显示层进行刷新
    }
}
