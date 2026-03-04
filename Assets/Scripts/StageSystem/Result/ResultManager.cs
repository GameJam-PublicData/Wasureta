using System;
using System.Collections.Generic;
using InputSystemActions;
using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.Saves;
using MainSystem.Scene;
using MainSystem.StageData;
using StageSystem.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    
    [Header("クリア画像")]
    [SerializeField] Image backgroundImage;
    [SerializeField] Image characterImage;
    
    [Header("ジングル")]
    [SerializeField] string clearJingleName;
    [SerializeField] string failedJingleName;
    
    //[SerializeField] Transform getItemsParent;
    //[SerializeField] GameObject getItemPrefab;
    [Inject] ISceneLoader _sceneLoader;
    [Inject] ISavesManager _savesManager;
    [Inject] IStageSOProvider _stageSOProvider;
    [Inject] IAudioManager _audioManager;

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
        
        //ジングル&BGM
        Debug.Log($"ResultManager AudioManager : {_audioManager}");
        _audioManager.StopBGM(); // 現在のBGMを即停止
        if (isClear)
        {
            _audioManager.PlayJingle(clearJingleName);
            _audioManager.PlayBGM(stageSO.DialogBGMName);
        }
        else
        {
            _audioManager.PlayJingle(failedJingleName);
            _audioManager.PlayBGM("Failed");
        }
        
        //目的地背景画像
        backgroundImage.sprite = stageSO.ClearImage;
        
        //キャラクター画像
        if (isClear)
        {
            characterImage.sprite = stageSO.ClearCharacterImage;
        }
        else
        {
            characterImage.sprite = stageSO.CharacterImage;
        }

        var timeLimit = stageSO.StageTimeLimit;
        //クリアタイムがタイムリミットを超えている場合はタイムリミットをクリアタイムとする
        if (clearTime > timeLimit) clearTime = timeLimit;
        
        // 9999点満点でスコアを計算
        int finalScore = CalculateScore(score, clearTime, timeLimit);
        
        stageTimeText.text = $"Time : {clearTime:F2}";
        scoreText.text = $"スコア : {finalScore}";
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
    
    int CalculateScore(int score, double clearTime, double timeLimit)
    {
        // アイテムスコアは最大6000点（3点×2000点）
        int itemScore = score * 2000;
        
        // 時間スコアは補助的に（最大3999点）
        int timeScore = (int)((timeLimit - clearTime) / timeLimit * 3999);
        timeScore = Mathf.Max(0, timeScore);
        
        return Mathf.Clamp(itemScore + timeScore, 0, 9999);
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