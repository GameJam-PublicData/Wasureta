using MainSystem.CoreFlow;
using MainSystem.Dialog;
using MainSystem.UIExample;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class DialogLifeTimeScope : LifetimeScope
{
    [SerializeField] DialogView dialogView;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(dialogView);
        builder.RegisterEntryPoint<DialogSystem>();
        builder.Register<IFade, Fade>(Lifetime.Scoped);
    }
    
    void Start()
    {
        Container.Resolve<IStageSelectManager>().SelectStage(0);
    }
}
}