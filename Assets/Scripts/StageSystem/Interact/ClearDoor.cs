using System;
using MainSystem.CoreFlow;
using StageSystem.Item;
using UnityEngine;
using VContainer;

namespace StageSystem.Interact
{
// ゲームを強制的に終了するドア
public class ClearDoor : MonoBehaviour, IInteractive
{
    IStageFlow _stageFlow;
    [Inject]
    public void Construct(IStageFlow stageFlow)
    {
        //必要な依存関係をここで注入する
    }
    
    public void Interact()
    {
        //ゲームクリアの処理をここに書く
        Debug.Log("ゲームエンド！");
        _stageFlow.EndStage();
    }
}
}
  
  
