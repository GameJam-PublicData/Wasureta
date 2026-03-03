using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MainSystem.UIExample
{
public class Fade : IFade
{
    Image _fadeImage;
    
    public void Init(Image fadeImage) => _fadeImage = fadeImage;

    public void FadeIn(float duration = 1, Action callback = null)
    {
        _fadeImage.color = new Color(0, 0, 0, 0);
        _fadeImage.DOFade(1, duration).OnComplete(() => callback?.Invoke());
    }
}
}