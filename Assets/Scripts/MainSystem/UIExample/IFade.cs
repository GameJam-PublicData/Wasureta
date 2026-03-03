using System;
using UnityEngine.UI;

namespace MainSystem.UIExample
{
public interface IFade
{
    void Init(Image fadeImage);
    void FadeIn(float duration, Action callback);
}
}