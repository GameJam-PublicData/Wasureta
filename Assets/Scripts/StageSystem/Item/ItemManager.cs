using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MainSystem.CoreFlow;
using MainSystem.StageData;
using UnityEngine;

namespace StageSystem.Item
{
public interface IItemManager
{
    void AddItem(IItem item);
    (List<IItem> lostItems, List<IItem> otherItems) GetItems();
    bool IsClear();
    Action<List<IItem>> OnGetItems { get; set; }
}

public class ItemManager : IItemManager
{
    ItemSO _itemSO;

    public ItemManager(IStageSOProvider stageSOProvider)
    {
        _itemSO = stageSOProvider.Get.ItemSO;
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
        item.GotItem();
        OnGetItems.Invoke(_itemList);
    }

    public Action<List<IItem>> OnGetItems { get; set; } = _ => { };



    public (List<IItem> lostItems, List<IItem> otherItems) GetItems()
    {
        List<IItem> lostItems = new();
        List<IItem> otherItems = new();

        foreach (var item in _itemList)
        {
            if (_itemSO.LostIItemList.Contains(item))
            {
                if(!lostItems.Contains(item)) lostItems.Add(item);
            }
            else
            {
                otherItems.Add(item);
            }
        }
        return (lostItems, otherItems);
    }


    //TODO:上は元の、下は新しいの
    /*
    public (List<IItem> lostItems, List<IItem> otherItems) GetItems()
    {
        List<IItem> lostItems = new();
        List<IItem> otherItems = new();

        foreach (Item item in _itemList)
        {
            bool isLostItem = false;

            foreach (Item itemSOItem in _itemSO.LostIItemList)
            {
                if (item.ItemName == itemSOItem.ItemName)
                {
                    if(!lostItems.Contains(item)) lostItems.Add(item);
                    isLostItem = true;
                }
            }

            if(isLostItem == false)
            {
                otherItems.Add(item);
            }
        }
        return (lostItems, otherItems);
    }
    */

    public bool IsClear()
    {
        var (lostItems, _) = GetItems();
        return lostItems.Count == _itemSO.LostIItemList.Count;
    }
}
}