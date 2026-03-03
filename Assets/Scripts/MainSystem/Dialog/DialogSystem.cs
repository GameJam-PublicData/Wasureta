using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MainSystem.CoreFlow;
using MainSystem.Scene;
using MainSystem.StageData;
using MainSystem.UIExample;
using TMPro;
using UnityEngine;
using VContainer.Unity;

namespace MainSystem.Dialog
{ 
public class DialogSystem : IPostStartable
{
    TextMeshProUGUI _dialogText;
    TextMeshProUGUI _rushIntroText;
    TextMeshProSimpleAnimator _dialogAnimator;
    TextMeshProSimpleAnimator _rushIntroAnimator;
    ISceneLoader _sceneLoader;
    IStageSOProvider _stageSOProvider;
    IFade _fade;
    StageSO _stageSO;
    
    public DialogSystem(DialogView dialogView, ISceneLoader sceneLoader, IStageSOProvider stageSOProvider, IFade fade)
    {
        Debug.Log("DialogSystem Construct");
        // DialogViewからテキストコンポーネントを取得
        _dialogText = dialogView.DialogText;
        _rushIntroText = dialogView.RushIntroText;
        // 依存関係の注入
        _sceneLoader = sceneLoader;
        _stageSOProvider = stageSOProvider;
        _fade = fade;
    }
    
    public void PostStart() => ProcessDialogueSceneFlow().Forget();

    // 会話シーンのフローを処理するメイン関数
    public async UniTask ProcessDialogueSceneFlow()
    {
        Init();
        
        var tokenSource = new CancellationTokenSource();
        var dialogSO = _stageSO.DialogSO;
        
        // 会話を開始
        await ShowDialogueAsync(dialogSO, DialogState.Dialog, tokenSource.Token);

        // 会話が終了したらフェードイン
        await _fade.FadeIn(1.25f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: tokenSource.Token);
        
        // フェードインが完了したら、当日のテキストを表示
        await ShowDayTransitionAsync(_stageSO.Title, tokenSource.Token);
        await ShowDialogueAsync(dialogSO, DialogState.RushIntroContent, tokenSource.Token);
        
        // ステージシーンへ遷移
        _sceneLoader.LoadScene(SceneType.StageScene).Forget();
    }

    public void Init()
    {
        _dialogText.gameObject.SetActive(true);
        _rushIntroText.gameObject.SetActive(false);
        
        _stageSO = _stageSOProvider.Get;
        _dialogAnimator = _dialogText.GetComponent<TextMeshProSimpleAnimator>();
        _rushIntroAnimator = _rushIntroText.GetComponent<TextMeshProSimpleAnimator>();
        
        if (_stageSO == null) throw new Exception("StageSO not set in DialogSystem");
        if (_dialogAnimator == null) throw new Exception("Dialog Animator not set");
        if (_rushIntroAnimator == null) throw new Exception("Rush Intro Animator not set");
    }
    
    enum DialogState
    {
        Dialog,
        RushIntroContent
    }
    
    async UniTask ShowDialogueAsync(DialogSO dialogSO, DialogState dialogState, CancellationToken token)
    {
        TextMeshProUGUI text = null;
        TextMeshProSimpleAnimator animator = null;
        string content = "";
        
        switch (dialogState)
        {
            case DialogState.Dialog:
                text = _dialogText;
                animator = _dialogAnimator;
                content = dialogSO.DialogContent;
                break;
            case DialogState.RushIntroContent:
                text = _rushIntroText;
                animator = _rushIntroAnimator;
                content = dialogSO.RushIntroContent;
                break;
        }
        
        if (text == null || animator == null) 
            throw new Exception("Text or Animator not set for the given DialogState");
        
        var charInterval = dialogSO.CharInterval;
        var lineEndDelay = dialogSO.LineEndDelay;
        
        animator.speedPerCharacter = charInterval;

        // セリフ単位に分割
        string[] contentLines = content.Split("{\\n}");
        foreach (string line in contentLines)
        {
            text.text = line;
            await ShowDialogueLineAsync(text, animator, line, lineEndDelay, token);
        }
    }

    async UniTask ShowDialogueLineAsync
        (TextMeshProUGUI text, TextMeshProSimpleAnimator animator, string line, float lineEndDelay, CancellationToken token)
    {
        Debug.Log(line.Replace("\\n", "\n"));
        
        // セリフを徐々に表示
        text.text = line;
        animator.Play();
        await UniTask.WaitUntil(() => !animator.isAnimating, cancellationToken: token);
        
        // セリフを全文表示してからの待機時間
        await UniTask.Delay(TimeSpan.FromSeconds(lineEndDelay), cancellationToken: token);
    }
    
    async UniTask ShowDayTransitionAsync(string stageTitle, CancellationToken token)
    {
        _rushIntroText.text = stageTitle + "当日...";
        _rushIntroText.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: token);
    }
}
}