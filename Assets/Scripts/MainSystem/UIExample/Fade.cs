using Cysharp.Threading.Tasks;
using DG.Tweening;
using MainSystem.UIExample;
using UnityEngine;
using UnityEngine.UI;

public class Fade : IFade
{
    Image _fadeImage;

    public void Init(Image fadeImage) => _fadeImage = fadeImage;

    public async UniTask FadeIn(float duration = 1f)
    {
        _fadeImage.color = new Color(0, 0, 0, 0);
        await _fadeImage.DOFade(1f, duration).AsyncWaitForCompletion();
    }
}