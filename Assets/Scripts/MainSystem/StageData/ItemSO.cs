using System.Collections.Generic;
using MainSystem.Item;
using UnityEngine;

namespace MainSystem.StageData
{
[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] List<Item.Item> itemList = new();
    
    public List<Item.Item> LostItemList => itemList;
    public List<IItem> LostIItemList => itemList.ConvertAll(item => item as IItem);
}
}