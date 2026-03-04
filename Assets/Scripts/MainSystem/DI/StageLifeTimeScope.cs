using Cysharp.Threading.Tasks;
using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.Scene;
using MainSystem.UIExample;
using StageSystem.Item;
using StageSystem.Result;
using StageSystem.Timer;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class StageLifeTimeScope : LifetimeScope
{
    [SerializeField] Image fadeImage;
    
    protected override void Configure(IContainerBuilder builder)
    {
        // StageSceneに特化した依存関係の登録をここに追加
        //StageFlow
        builder.Register<IStageFlow,StageFlow>(Lifetime.Scoped);
        builder.Register<ITimeManager,TimeManager>(Lifetime.Scoped);
        
        //Item
        builder.RegisterComponentInHierarchy<IAudioManager>();

        builder.Register<IItemManager, ItemManager>(Lifetime.Scoped);
        
        //result
        builder.RegisterComponentInHierarchy<IResultManager>();

        //Dialog
        builder.RegisterInstance(fadeImage).Keyed("FadeImage");
        builder.Register<IFade, Fade>(Lifetime.Scoped);
    }

    void Start()
    {
        SceneInitialization().Forget();
    }

    // StageSceneの初期化処理をここに追加
    async UniTask SceneInitialization()
    {
        var pub = Container.Resolve<ISceneInitializationPublisher>();
        
        //ここでStageSceneの初期化処理を行う。例えば、UIのセットアップや、必要なデータのロードなど。

        Container.Resolve<IStageFlow>().StartStage();
        await UniTask.Yield();//ダミー
        
        // DialogSceneからのフェードアウト
        var fade = Container.Resolve<IFade>();
        await fade.FadeOut(1.5f);
        
        // 初期化が完了したら、ISceneInitializationPublisherを通じて、初期化が完了したことを通知します。
        pub.NotifyInitializationComplete();
    }
}
}
