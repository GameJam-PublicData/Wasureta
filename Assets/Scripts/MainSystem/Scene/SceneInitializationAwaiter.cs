using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainSystem.Scene
{
public interface ISceneInitializationPublisher
{
    void NotifyInitializationComplete();
    void NotifyInitializationFailed(Exception ex);
}
public interface ISceneInitializationAwaiter
{
    UniTask WaitForInitialization();
    void Reset();
}

public class SceneInitializationAwaiter : ISceneInitializationAwaiter, ISceneInitializationPublisher
{
    UniTaskCompletionSource _tcs;
    
    public async UniTask WaitForInitialization()
    {
        _tcs = new UniTaskCompletionSource();

        try
        {
            await _tcs.Task;
        }
        catch (Exception e)
        {
            Debug.LogError($"シーンの初期化が失敗しました: {e.Message}");
            throw;
        }
    }
    
    public void NotifyInitializationComplete()
    {
        Debug.Log("シーンの初期化が完了しました");
        if(_tcs == null)
        {
            Debug.LogWarning("シーンの初期化が完了しましたが、待機中のタスクがありませんでした。");
            return;
        }
        _tcs.TrySetResult();
    }
    
    public void NotifyInitializationFailed(Exception ex)
    {
        Debug.LogError($"シーンの初期化が失敗しました: {ex.Message}");
        if(_tcs == null)        {
            Debug.LogWarning("シーンの初期化が失敗しましたが、待機中のタスクがありませんでした。");
            return;
        }
        _tcs.TrySetException(ex);
    }
    
    public void Reset()
    {
        _tcs = new UniTaskCompletionSource();
    }
}
}