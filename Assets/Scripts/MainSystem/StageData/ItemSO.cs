using System.Collections.Generic;
using StageSystem.Item;
using UnityEngine;

namespace MainSystem.StageData
{
[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] List<Item> itemList = new();
    
    public List<Item> LostItemList => itemList;
    public List<IItem> LostIItemList => itemList.ConvertAll(item => item as IItem);
}
}