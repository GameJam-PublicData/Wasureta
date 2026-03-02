using System.Collections.Generic;
using MainSystem.StageData;
using UnityEngine;

namespace StageSystem.Item
{
public interface IItemManager
{
    void AddItem(IItem item);
    (List<IItem> lostItems, List<IItem> getItems) GetItems();
}

public class ItemManager : IItemManager
{
    ItemSO _itemSO;

    public ItemManager(ItemSO itemSO)
    {
        _itemSO = itemSO;
    }

    List<IItem> _itemList = new();

    public void AddItem(IItem item)
    {
        if (_itemList.Contains(item))
        {
            Debug.LogError("同じアイテムが既に存在しています。");
            return;
        }

        _itemList.Add(item);
    }

    public (List<IItem> lostItems, List<IItem> getItems) GetItems()
    {
        List<IItem> lostItems = new();
        List<IItem> otherItems = new();

        foreach (var item in _itemList)
        {
            if (_itemSO.LostIItemList.Contains(item))
            {
                lostItems.Add(item);
            }
            else
            {
                otherItems.Add(item);
            }
        }

        return (lostItems, otherItems);
    }
}
}