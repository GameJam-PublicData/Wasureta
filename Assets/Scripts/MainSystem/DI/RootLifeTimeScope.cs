using System.Collections.Generic;
using MainSystem.CoreFlow;
using MainSystem.Saves;
using MainSystem.Scene;
using MainSystem.StageData;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class RootLifeTimeScope : LifetimeScope
{
    [SerializeField] bool notLoadBootScene = true;
    [SerializeField] List<StageSO> stages;
    protected override void Configure(IContainerBuilder builder)
    {
        // builder.Register<interface,class>();
        builder.Register<BootManager>(Lifetime.Singleton);
        builder.Register<ISceneLoader,SceneLoader>(Lifetime.Singleton);
        builder.Register<SceneInitializationAwaiter>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<StageSelectManager>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ISavesManager, SaveManager>(Lifetime.Singleton);
        
        builder.RegisterInstance(stages).Keyed("AllStages");
    }

    void Start()
    {
        var bootManager = Container.Resolve<BootManager>();
        bootManager.Initialize(notLoadBootScene);
    }
}
}