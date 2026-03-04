using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MainSystem.StageData;
using StageSystem.Item;
using StageSystem.Result;
using StageSystem.Timer;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MainSystem.CoreFlow
{
//Findステージでのフローを管理するクラス
public interface IStageFlow
{
    void StartStage();
    void EndStage(bool isTimerEnd = false);
}

public class StageFlow : IStageFlow ,IDisposable
{
    StageSO _stageSO;
    IResultManager _resultManager;
    IItemManager _itemManager;
    ITimeManager _timeManager;
    
    CancellationTokenSource _stageCTS;

    public StageFlow(IStageSOProvider provider,IResultManager resultManager,IItemManager itemManager,ITimeManager timeManager)
    {
        _stageSO = provider.Get;
        _resultManager = resultManager;
        _itemManager = itemManager;
        _timeManager = timeManager;
    }
    
    public void StartStage()
    {
        //ステージ開始の処理
        Debug.Log("ステージ開始");
        _stageCTS = new CancellationTokenSource();
        Flow().Forget();
        
    }
    
    async UniTask Flow()
    {
        await UniTask.WhenAll(
            _timeManager.StartTimer(_stageSO.StageTimeLimit,_stageCTS.Token)
        );
        EndStage(true);//GameOver  クリアは別の処理
    }
    public static bool IsGameEnd { get;private set; }
    
    public void EndStage(bool isTimerEnd = false)
    {
        
        _timeManager.StopTimer();
        //ステージ終了の処理
        Debug.Log("ステージ終了");
        _stageCTS.Cancel();

        var items = _itemManager.GetItems();
        
        var allLostItemCount = _stageSO.ItemSO.LostIItemList.Count;
        var lostItemCount = items.lostItems.Count;
        var otherItemCount = items.otherItems.Count;

        var score = lostItemCount < allLostItemCount ? 1 // 忘れ物を持ち出せなかった
            : otherItemCount > 0 ? 2                     // 忘れ物と一緒に違うものも持ってきた
            : 3;                                         // 忘れ物を全て正しく持ち出せた

        _resultManager.SetResult(
            _itemManager.IsClear(),   //todo クリア判定
            _stageSO,
            _timeManager.GetElapsedTime(),
            isTimerEnd ? 0 : score,
            items.otherItems,
            items.lostItems
        );
        IsGameEnd = true;
    }


    public void Dispose()
    {
        _stageCTS?.Dispose();
    }
}
}