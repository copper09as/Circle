using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GainMagic:MonoBehaviour 
{
    public static async Task<Magic> GainSO(string name)//��ȡ��Ӧħ����SO
    {
        var handle =Addressables.LoadAssetAsync<Magic>(name);
        await handle.Task;
        if (handle.IsValid())
            return handle.Result;
        else
        { Debug.Log("δ�ҵ�ħ��" + name); return null; }
    }
    public static async Task<GameObject> LoadCardPrefabAsync(GameObject Parent)//���ؿ���Ԥ����
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("Card");
        await handle.Task;
        if (handle.IsValid())
        {
            GameObject card = handle.Result;
            return Instantiate(card, Parent.transform);
            //����Ԥ����ʵ������ָ���������£������ظ�Ԥ����
        }
        else
        { Debug.Log("δ�ҵ�CardԤ����"); return null; }
    }
    public static async void GainCard(string magicName, GameObject Parent)//��ȡ����
    {
        GameObject card=await LoadCardPrefabAsync(Parent);
        CardDisplay display = card.GetComponent<CardDisplay>();
        //��ֵSO
        display.magic = await GainSO(magicName);

        display.Refresh();
        //�Կ�����ʾ�����ˢ��
    }
}
