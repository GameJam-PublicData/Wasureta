using System;
using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.StageData;
using UnityEditor.SceneManagement;
using UnityEngine;
using VContainer;

namespace StageSystem.Sound
{ 
public class BGMStart : MonoBehaviour
{
    IAudioManager _audioManager;
    StageSO _stageSO;
    const string StageBGMName = "BGM_Stage";
    
    [SerializeField] bool isDialogScene;
    
    [Inject]
    public void Construct(IAudioManager audioManager, IStageSOProvider stageSOProvider)
    {
        _audioManager = audioManager;
        _stageSO = stageSOProvider.Get;
    }

    void Start()
    {
        if (isDialogScene)
        {
            //ダイアログシーンのBGM再生(ステージごとにBGM変化)
            _audioManager.PlayBGM(_stageSO.BGM.SoundName);
        }
        else
        {
            //ステージシーンのBGM再生
            _audioManager.PlayBGM(StageBGMName);
        }
    }
}
}
