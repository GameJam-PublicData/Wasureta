using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MainSystem.StageData;
using StageSystem.Item;
using StageSystem.Result;
using StageSystem.Timer;
using Debug = UnityEngine.Debug;

namespace MainSystem.CoreFlow
{
//Findステージでのフローを管理するクラス
public interface IStageFlow
{
    void StartStage();
    void EndStage();
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
        EndStage();//GameOver  クリアは別の処理
    }
    
    public void EndStage()
    {
        
        _timeManager.StopTimer();
        //ステージ終了の処理
        Debug.Log("ステージ終了");
        _stageCTS.Cancel();
        
        _resultManager.SetResult(
            _itemManager.IsClear(),   //todo クリア判定
            _stageSO,
            _timeManager.GetElapsedTime(),
            0,   //todo スコア
            _itemManager.GetItems().getItems //todo 取得アイテム
        );
    }


    public void Dispose()
    {
        _stageCTS?.Dispose();
    }
}
}