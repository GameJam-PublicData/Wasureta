using System;
using System.Collections.Generic;
using InputSystemActions;
using MainSystem.CoreFlow;
using MainSystem.Saves;
using MainSystem.Scene;
using MainSystem.StageData;
using StageSystem.Item;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using VContainer;

namespace StageSystem.Result
{

public interface IResultManager
{
    void SetResult(
        bool isClear,
        StageSO stageSO,
        double clearTime, 
        int score,
        List<IItem> getItems,
        List<IItem> lostItems);
}

public class ResultManager : MonoBehaviour, IResultManager
{
    [SerializeField] List<GameObject> resultStars = new();
    [SerializeField] TextMeshProUGUI stageTimeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI getItemsText;

    [SerializeField] GameObject clearImage;
    [SerializeField] GameObject failedImage;
    //[SerializeField] Transform getItemsParent;
    //[SerializeField] GameObject getItemPrefab;
    [Inject] ISceneLoader _sceneLoader;
    [Inject] ISavesManager _savesManager;
    [Inject] IStageSOProvider _stageSOProvider;

    int _score;
    public void SetResult(
        bool isClear,
        StageSO stageSO,
        double clearTime,
        int score,
        List<IItem> getItems,
        List<IItem> lostIt)
    {
        //todo 結果画面にステージデータ、クリアタイム、スコアを渡す
        gameObject.SetActive(true);
        
        
        _score = score;
        //クリア画像
        if (isClear)
        {
            clearImage.SetActive(true);
            failedImage.SetActive(false);
        }
        else
        {
            clearImage.SetActive(false);
            failedImage.SetActive(true);
        }
        
        stageTimeText.text = $"クリア時間 : {clearTime:F2}";
        scoreText.text = $"スコア : {score}";
        getItemsText.text =$"持ち物 :{lostIt.Count}/{stageSO.ItemSO.LostItemList.Count}";
        
        SetStarts(score);

        /*
         //todo 獲得アイテムの表示処理
        foreach (var item in getItems)
        {
            GameObject itemObj = Instantiate(getItemPrefab, getItemsParent);
            itemObj.GetComponent<ItemViewer>().SetItem(item);
        }*/
        
        _inputActions = new InputActions();
        
        _inputActions.UI.Enable();
        _inputActions.UI.Submit.started += GoNext;
    }

    void SetStarts(int starCount)
    {
        for (int i = 0; i< starCount; i++)
        {
            resultStars[i].SetActive(true);
        }
    }

    InputActions _inputActions;

    void GoNext(InputAction.CallbackContext ctx)
    {
        Debug.Log("次の画面へ");
        _savesManager.SaveStageScore(_stageSOProvider.Get.StageIndex, _score);
        _sceneLoader.LoadScene(SceneType.MainMenuScene);
    }

    void OnDisable()
    {
        if(_inputActions == null) return;
        _inputActions.UI.Submit.started -= GoNext;
        _inputActions.Disable();
    }
}
}