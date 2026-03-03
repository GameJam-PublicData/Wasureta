using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MainSystem.CoreFlow;
using MainSystem.Scene;
using MainSystem.StageData;
using TMPro;
using UnityEngine;
using VContainer;

namespace MainSystem.Dialog
{
public interface IDialogSystem
{
    UniTask ProcessDialogueSceneFlow();
}
public class DialogSystem : IDialogSystem
{
    TextMeshProUGUI _dialogText;
    TextMeshProSimpleAnimator _dialogAnimator;
    ISceneLoader _sceneLoader;
    StageSO _stageSO;
    
    [Inject]
    public void Construct([Key("DialogText")]TextMeshProUGUI dialogText, ISceneLoader sceneLoader, IStageSOProvider stageSOProvider)
    {
        Debug.Log("DialogSystem Construct");
        _dialogText = dialogText;
        _sceneLoader = sceneLoader;
        _stageSO = stageSOProvider.Get;
    }

    // 会話シーンのフローを処理するメイン関数
    public async UniTask ProcessDialogueSceneFlow()
    {
        Init();
        
        var tokenSource = new CancellationTokenSource();
        var dialogSO = _stageSO.DialogSO;
        
        await StartDialog(dialogSO, tokenSource.Token);
        
        _sceneLoader.LoadScene(SceneType.StageScene).Forget();
    }
    
    public void Init()
    {
        _dialogAnimator = _dialogText.GetComponent<TextMeshProSimpleAnimator>();
        if (_dialogAnimator == null)
        {
            Debug.LogError("TextMeshProSimpleAnimator component is missing on the dialog text object.");
        }
    }

    // TODO: 会話が終了次第、コールバック / イベントで通知
    public async UniTask StartDialog(DialogSO dialogSO, CancellationToken token)
    {
        string content = dialogSO.Content;
        float charInterval = dialogSO.CharInterval;
        float lineEndDelay = dialogSO.LineEndDelay;

        // 一文字あたりの表示時間
        _dialogAnimator.speedPerCharacter = charInterval;

        // セリフ単位に分割
        string[] contentLines = content.Split("{\\n}");
        foreach (string line in contentLines)
        {
            await ShowDialogLine(line, lineEndDelay, token); // セリフを表示
        }
    }

    async UniTask ShowDialogLine(string line, float lineEndDelay, CancellationToken token)
    {
        Debug.Log(line);
        
        // セリフを徐々に表示
        _dialogText.text = line;
        _dialogAnimator.Play();
        await UniTask.WaitUntil(() => !_dialogAnimator.isAnimating, cancellationToken: token);
        
        // セリフを全文表示してからの待機時間
        await UniTask.Delay(TimeSpan.FromSeconds(lineEndDelay), cancellationToken: token);
        _dialogText.text = "";
    }
}
}