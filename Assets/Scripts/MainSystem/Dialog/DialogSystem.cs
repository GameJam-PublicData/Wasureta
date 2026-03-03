using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;

namespace MainSystem.Dialog
{
public interface IDialogSystem
{
    void Init();
    UniTask ShowDialog(DialogSO dialogSO, CancellationToken token);
}
public class DialogSystem : IDialogSystem
{
    TextMeshProUGUI _dialogText;
    TextMeshProSimpleAnimator _dialogAnimator;
    
    [Inject]
    public void Construct([Key("DialogText")]TextMeshProUGUI dialogText)
    {
        Debug.Log("DialogSystem Construct");
        _dialogText = dialogText;
    }

    public void Init()
    {
        _dialogAnimator = _dialogText.GetComponent<TextMeshProSimpleAnimator>();
    }

    // TODO: 会話が終了次第、コールバック / イベントで通知
    public async UniTask ShowDialog(DialogSO dialogSO, CancellationToken token)
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
        
        _dialogText.text = line;
        _dialogAnimator.Play();
        await UniTask.WaitUntil(() => !_dialogAnimator.isAnimating, cancellationToken: token);
        
        // セリフを全文表示してからの待機時間
        await UniTask.Delay(TimeSpan.FromSeconds(lineEndDelay), cancellationToken: token);
        _dialogText.text = "";
    }
}
}