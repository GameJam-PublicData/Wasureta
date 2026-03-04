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

   // ItemSO.cs の OnValidate メソッドを以下に置き換え
    void OnValidate()
    {
        if (itemRootPrefab == null)
        {
            Debug.LogError("アイテムのルートプレハブが設定されていません。");
            return;
        }
    
        itemList.Clear(); // 毎回リストをクリアして再構築
    
        for (int i = 0; i < itemRootPrefab.transform.childCount; i++)
        {
            var child = itemRootPrefab.transform.GetChild(i).gameObject;
            var itemComponent = child.GetComponent<Item>();
            if (itemComponent == null)
            {
                Debug.LogError($"子オブジェクト '{child.name}' に Item コンポーネントがアタッチされていません。");
                continue;
            }
    
            itemList.Add(itemComponent);
        }
    
        Debug.Log($"ItemSO: アイテムリスト更新完了。合計 {itemList.Count} 個");
    }


    public List<Item> LostItemList => itemList;
    public List<IItem> LostIItemList => itemList.ConvertAll(item => item as IItem);
}
}