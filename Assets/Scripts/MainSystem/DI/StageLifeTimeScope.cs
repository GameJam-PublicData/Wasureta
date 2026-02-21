using System;
using Cysharp.Threading.Tasks;
using MainSystem.DI.Installer;
using MainSystem.Scene;
using VContainer;
using VContainer.Unity;

namespace MainSystem.DI
{
public class StageLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // StageSceneに特化した依存関係の登録をここに追加
        
    }

    void Start()
    {
        SceneInitialization().Forget();
    }

    async UniTask SceneInitialization()
    {
        // StageSceneの初期化処理をここに追加
        var pub = Container.Resolve<ISceneInitializationPublisher>();
        await UniTask.Yield();//ダミー
        pub.NotifyInitializationComplete();
    }
}
}
