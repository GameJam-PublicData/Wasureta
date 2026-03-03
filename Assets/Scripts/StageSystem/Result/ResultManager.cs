using System;
using System.Collections.Generic;
using MainSystem.StageData;
using StageSystem.Item;
using TMPro;
using UnityEngine;

namespace StageSystem.Result
{

public interface IResultManager
{
    void SetResult(
        bool isClear,
        StageSO stageSO,
        double clearTime, 
        int score,
        List<IItem> getItems);
}

public class ResultManager : MonoBehaviour, IResultManager
{
    [SerializeField] List<GameObject> resultStars = new();
    [SerializeField] TextMeshProUGUI clearText;
    [SerializeField] TextMeshProUGUI stageTimeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI getItemsText;
    //[SerializeField] Transform getItemsParent;
    //[SerializeField] GameObject getItemPrefab;


    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetResult(
        bool isClear,
        StageSO stageSO,
        double clearTime,
        int score,
        List<IItem> getItems)
    {
        //todo 結果画面にステージデータ、クリアタイム、スコアを渡す
        gameObject.SetActive(true);
        clearText.text = isClear ? "Clear!" : "Failed";
        clearText.color = isClear ? Color.green : Color.red;
        stageTimeText.text = $"ClearTime: {clearTime:F2}";
        scoreText.text = $"Score: {score}";
        getItemsText.text =$"GetItems:{getItems.Count}/{stageSO.ItemSO.LostItemList.Count}";
        
        SetStarts();

        /*
         //todo 獲得アイテムの表示処理
        foreach (var item in getItems)
        {
            GameObject itemObj = Instantiate(getItemPrefab, getItemsParent);
            itemObj.GetComponent<ItemViewer>().SetItem(item);
        }*/
    }

    void SetStarts()
    {
        //todo 星の数の計算処理
        int starCount = 2;
        for (int i = 0; i< starCount; i++)
        {
            resultStars[i].SetActive(true);
        }
    }
}
}