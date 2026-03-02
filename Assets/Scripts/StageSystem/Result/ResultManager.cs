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
        StageSO stageSO,
        double clearTime, 
        int score,
        List<IItem> getItems);
}

public class ResultManager : MonoBehaviour, IResultManager
{
    [SerializeField] List<GameObject> resultStars = new();
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
        StageSO stageSO,
        double clearTime,
        int score,
        List<IItem> getItems)
    {
        //todo 結果画面にステージデータ、クリアタイム、スコアを渡す
        gameObject.SetActive(true);
        stageTimeText.text = $"クリアタイム: {clearTime:F2}秒";
        scoreText.text = $"スコア: {score}";
        getItemsText.text =$"獲得アイテム:{getItems.Count}/{stageSO.ItemSO.LostItemList.Count}";
        
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