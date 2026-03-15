using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.StageData;
using UnityEngine;
using VContainer;

namespace StageSystem.Sound
{
public class BGMStart : MonoBehaviour
{
    IAudioManager _audioManager;
    StageSO _stageSO;

    [SerializeField] bool isDialogScene;

    [Header("シーン上で指定")]
    [SerializeField] bool useCustomBGM;
    [SerializeField] string bgmName;

    [Inject]
    public void Construct(IAudioManager audioManager, IStageSOProvider stageSOProvider)
    {
        _audioManager = audioManager;
        _stageSO = stageSOProvider.Get;
    }

    void Start()
    {
        if (useCustomBGM)
        {
            _audioManager.PlayBGM(bgmName);
            return;
        }

        if (isDialogScene)
        {
            _audioManager.PlayBGM(_stageSO.DialogBGMName);
        }
        else
        {
            _audioManager.PlayBGM(_stageSO.StageBGMName, _stageSO.StageBgmFadeInTime);
        }
    }
}
}