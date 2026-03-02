using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MainSystem.StageData;
using UnityEngine;

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
    CancellationTokenSource _stageCTS;

    public StageFlow(StageSO stageSO)
    {
        _stageSO = stageSO;
    }
    
    
    public void StartStage()
    {
        //ステージ開始の処理
        Debug.Log("ステージ開始");
        _stageCTS = new CancellationTokenSource();
        StageTimeLimit(_stageCTS.Token).Forget();
    }

    async UniTask StageTimeLimit(CancellationToken token)
    {
        //ステージの制限時間の処理
        await UniTask.Delay(TimeSpan.FromSeconds(_stageSO.StageTimeLimit), cancellationToken: token);
        EndStage();
    }


    public void EndStage()
    {
        //ステージ終了の処理
        Debug.Log("ステージ終了");
        _stageCTS.Cancel();
    }


}
}