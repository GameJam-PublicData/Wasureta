using System;
using System.Diagnostics;
using System.Threading;
using Cysharp.Threading.Tasks;
using MainSystem.StageData;
using StageSystem.Item;
using StageSystem.Result;
using Debug = UnityEngine.Debug;

namespace MainSystem.CoreFlow
{
//Findステージでのフローを管理するクラス
public interface IStageFlow
{
    void StartStage();
    void EndStage();
}

public class StageFlow : IStageFlow
{
    StageSO _stageSO;
    IResultManager _resultManager;
    IItemManager _itemManager;
    
    CancellationTokenSource _stageCTS;

    public StageFlow(StageSO stageSO,IResultManager resultManager,IItemManager itemManager)
    {
        _stageSO = stageSO;
        _resultManager = resultManager;
        _itemManager = itemManager;
    }
    
    
    public void StartStage()
    {
        //ステージ開始の処理
        Debug.Log("ステージ開始");
        _stageCTS = new CancellationTokenSource();
        StageTimeLimit(_stageCTS.Token).Forget();
    }


    Stopwatch _stopwatch = new();
    async UniTask StageTimeLimit(CancellationToken token)
    {
        //ステージの制限時間の処理
        _stopwatch.Start();
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_stageSO.StageTimeLimit), cancellationToken: token);
        }
        finally
        {
            _stopwatch.Stop();
             Debug.Log($"ステージ制限時間終了。経過時間: {_stopwatch.Elapsed.TotalSeconds}秒");
        }
      
        EndStage();
    }


    public void EndStage()
    {
        //ステージ終了の処理
        Debug.Log("ステージ終了");
        _stageCTS.Cancel();
        
        _resultManager.SetResult(
            _stageSO,
            _stopwatch.Elapsed.TotalSeconds,
            0,   //todo スコア
            _itemManager.GetItems().getItems //todo 取得アイテム
        );
    }


}
}