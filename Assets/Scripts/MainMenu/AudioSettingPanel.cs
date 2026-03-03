using System.Collections;
using MainSystem.Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using VContainer;

namespace MainMenu
{
public class AudioSettingPanel : MonoBehaviour
{
    IAudioManager _audioManager;

    [Inject]
    public void Construct(IAudioManager audioManager)
    {
        Debug.LogError("AudioSettingPanelにAudioManagerが注入されました");
        _audioManager = audioManager;
    }

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Button closeButton;

    void Start()
    {
        masterSlider.value = VolumeToSlider(_audioManager.GetVolume(AudioCategory.Master));
        bgmSlider.value = VolumeToSlider(_audioManager.GetVolume(AudioCategory.BGM));
        seSlider.value = VolumeToSlider(_audioManager.GetVolume(AudioCategory.SE));

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);

        closeButton.onClick.AddListener(() =>
        {
            _audioManager.PlaySE("ButtonPush");
            gameObject.SetActive(false);
        });
    }

    void OnMasterVolumeChanged(float value)
    {
        _audioManager.SetVolume(AudioCategory.Master, SliderToVolume(value));
    }

    void OnBGMVolumeChanged(float value)
    {
        _audioManager.SetVolume(AudioCategory.BGM, SliderToVolume(value));
    }

    void OnSEVolumeChanged(float value)
    {
        _audioManager.SetVolume(AudioCategory.SE, SliderToVolume(value));
    }
    
    float SliderToVolume(float value)
    {
        if (value <= 0f)
            return -80f;

        if (value <= 0.5f)
        {
            // 0+ ~ 0.5 → -20 ~ 0(dB)
            float t = value / 0.5f;
            return Mathf.Lerp(-20f, 0f, t);
        }
        else
        {
            // 0.5 ~ 1.0 → 0 ~ 10(dB)
            float t = (value - 0.5f) / 0.5f;
            return Mathf.Lerp(0f, 10f, t);
        }
    }

    float VolumeToSlider(float volume)
    {
        if (volume <= -80f)
            return 0f;

        if (volume <= 0f)
        {
            // -20 ~ 0(dB) → 0 ~ 0.5
            float t = Mathf.InverseLerp(-20f, 0f, volume);
            return t * 0.5f;
        }
        else
        {
            // 0 ~ 10(dB) → 0.5 ~ 1.0
            float t = Mathf.InverseLerp(0f, 10f, volume);
            return 0.5f + t * 0.5f;
        }
    }
    
    
    /*
    
    float SliderToVolume(float value)
    {
        if(value <= 0f)
            return -80f;

        if (value <= 0.5)
        {
            //0.5以下の場合は、-80 ~ 5(dB)で直線
            float t = value / 0.5f;
            return Mathf.Lerp(-80f, 5f, t);
        }
        else
        {
            //0.5超過の場合は、5 ~ 15(dB)で直線
            float t = (value - 0.5f) / 0.5f;
            return Mathf.Lerp(5f, 15f, t);
        }
    }
    
    float VolumeToSlider(float volume)
    {
        if (volume <= -80f)
            return 0f;

        if (volume <= 5f)
        {
            //-80 ~ 5(dB)の範囲を0 ~ 0.5にマッピング
            float t = Mathf.InverseLerp(-80f, 5f, volume);
            return t * 0.5f;
        }
        else
        {
            //5 ~ 15(dB)の範囲を0.5 ~ 1にマッピング
            float t = Mathf.InverseLerp(5f, 15f, volume);
            return 0.5f + t * 0.5f;
        }
    }
    
    
    float SliderToVolume(float value)
    {
        return value > 0f
            ? Mathf.Log10(value) * _volumeScale
            : -80f;

    }
    
    float VolumeToSlider(float volume)
    {
        return volume <= -80f
            ? 0f
            : Mathf.Pow(10f, volume / _volumeScale);
    }*/
}
}