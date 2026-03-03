using Cysharp.Threading.Tasks;
using MainSystem.Dialog;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class DialogLifeTimeScope : LifetimeScope
{
    [SerializeField] TextMeshProUGUI dialogText;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IDialogSystem, DialogSystem>(Lifetime.Scoped);
        builder.RegisterInstance(dialogText).Keyed("DialogText");
    }
    
    void Start()
    {
        Container.Resolve<IDialogSystem>().ProcessDialogueSceneFlow().Forget();
    }
}
}