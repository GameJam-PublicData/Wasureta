using System;
using MainSystem.Audio;
using UnityEngine;
using VContainer;

namespace StageSystem.Sound
{ 
public class BGMStart : MonoBehaviour
{
    IAudioManager _audioManager;
    
    [Inject]
    public void Construct(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    void Start()
    {
        Debug.Log(_audioManager);
        _audioManager.PlayBGM("BGM_Stage");
    }
}
}
