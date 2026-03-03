using MainSystem.Audio;
using UnityEngine;
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
    //a
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Button closeButton;
    
    void Start()
    {
        masterSlider.value = _audioManager.GetVolume(AudioCategory.Master)/100f;
        bgmSlider.value = _audioManager.GetVolume(AudioCategory.BGM) / 100f;
        seSlider.value = _audioManager.GetVolume(AudioCategory.SE) / 100f;
        
        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
        
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
    
    void OnMasterVolumeChanged(float value)
    {
        _audioManager.SetVolume(AudioCategory.Master, value * 100f);
    }
    
    void OnBGMVolumeChanged(float value)
    {
        _audioManager.SetVolume(AudioCategory.BGM, value * 100f);
    }
    
    void OnSEVolumeChanged(float value)
    {
        _audioManager.SetVolume(AudioCategory.SE, value * 100f);
    }
}
}
  
  
