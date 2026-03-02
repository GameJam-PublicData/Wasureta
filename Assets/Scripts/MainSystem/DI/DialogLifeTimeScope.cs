using System;
using MainSystem.Dialog;
using TMPro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class DialogLifeTimeScope : LifetimeScope
{
    [SerializeField] TextMeshProUGUI _dialogText;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IDialogSystem, DialogSystem>(Lifetime.Scoped);
        builder.RegisterInstance(_dialogText).Keyed("DialogText");
    }

    void Start()
    {
        Container.Resolve<IDialogSystem>().Init();
    }
}
}