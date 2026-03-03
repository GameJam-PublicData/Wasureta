using System;
using System.Collections.Generic;
using StageSystem.Item;
using UnityEngine;

namespace MainSystem.StageData
{
[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] GameObject itemRootPrefab;
    [SerializeField,HideInInspector] List<Item> itemList = new();

    void OnValidate()
    {
        if (itemRootPrefab == null)
        {
            Debug.LogError("アイテムのルートプレハブが設定されていません。");
            return;
        }

        for (int i = 0; i < itemRootPrefab.transform.childCount; i++)
        {
            var child = itemRootPrefab.transform.GetChild(i).gameObject;
            var itemComponent = child.GetComponent<Item>();
            if (itemComponent == null)
            {
                Debug.LogError($"子オブジェクト '{child.name}' に Item コンポーネントがアタッチされていません。");
                continue;
            }

            if (!itemList.Contains(itemComponent))
            {
                itemList.Add(itemComponent);
            }
        }

    }


    public List<Item> LostItemList => itemList;
    public List<IItem> LostIItemList => itemList.ConvertAll(item => item as IItem);
}
}