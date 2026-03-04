using Cysharp.Threading.Tasks;

namespace MainSystem.UIExample
{
public interface IFade
{
    UniTask FadeIn(float duration);
}
}