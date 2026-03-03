using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace MainSystem.UIExample
{
public interface IFade
{
    void Init(Image fadeImage);
    UniTask FadeIn(float duration);
}
}