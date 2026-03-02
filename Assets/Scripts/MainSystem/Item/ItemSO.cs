using System.Collections.Generic;
using UnityEngine;

namespace MainSystem.Item
{
[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] List<Item> _itemList = new();
    
    public List<Item> LostItemList => _itemList;
    public List<IItem> LostIItemList => _itemList.ConvertAll(item => item as IItem);
}
}