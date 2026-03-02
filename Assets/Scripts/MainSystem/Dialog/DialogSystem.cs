using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainSystem.Dialog
{
public interface IDialogSystem
{
    void Init();
    void ShowDialog(DialogSO dialogSO);
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
    public async void ShowDialog(DialogSO dialogSO)
    {
        string content = dialogSO.Content;
        float viewTime = dialogSO.ViewTime;

        // 一文字あたりの表示時間
        _dialogAnimator.speedPerCharacter = viewTime;

        // セリフ単位に分割
        string[] contentLines = content.Split("next");
        foreach (string line in contentLines)
        {
            await ShowDialogLine(line); // セリフを表示
        }
    }

    async UniTask ShowDialogLine(string line)
    {
        Debug.Log(line);
        
        _dialogText.text = line;
        _dialogAnimator.Play();
        await UniTask.WaitUntil(() => !_dialogAnimator.isAnimating);
        
        // セリフを全文表示してからの待機時間
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _dialogText.text = "";
    }
}
}