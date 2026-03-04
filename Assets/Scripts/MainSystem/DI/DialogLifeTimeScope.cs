using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.Dialog;
using MainSystem.UIExample;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class DialogLifeTimeScope : LifetimeScope
{
    [SerializeField] DialogView dialogView;
    [SerializeField] Image fadeImage;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(dialogView);
        builder.RegisterInstance(fadeImage).Keyed("FadeImage");
        builder.RegisterEntryPoint<DialogSystem>();
        builder.Register<IFade, Fade>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<IAudioManager>();
    }
    
    void Start()
    {
        Container.Resolve<IStageSelectManager>().SelectStage(0);
    }
}
}