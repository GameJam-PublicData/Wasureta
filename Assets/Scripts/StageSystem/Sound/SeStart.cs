using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.StageData;
using UnityEngine;
using VContainer;

namespace StageSystem.Sound
{
public class SEStart : MonoBehaviour
{
    IAudioManager _audioManager;
    
    [SerializeField] string seName;

    [Inject]
    public void Construct(IAudioManager audioManager, IStageSOProvider stageSOProvider)
    {
        _audioManager = audioManager;
    }

    void Start()
    {
        _audioManager.PlaySE(seName);
    }
}
}
