using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NpcItems", menuName = "Npc/NpcItems")]
public class NpcItemList : ScriptableObject
{
    public List<ItemBagSo> npcItems;
}
