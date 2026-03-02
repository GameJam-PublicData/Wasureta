using System.Collections.Generic;
using StageSystem.Item;
using UnityEngine;

namespace MainSystem.StageData
{
[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] List<StageSystem.Item.Item> itemList = new();
    
    public List<StageSystem.Item.Item> LostItemList => itemList;
    public List<IItem> LostIItemList => itemList.ConvertAll(item => item as IItem);
}
}