using Cysharp.Threading.Tasks;
using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.Scene;
using MainSystem.StageData;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MainMenu
{

public interface IMainMenuHolder
{
    Button GameEndButton { get; }
    Button LicenseButton { get; }
    GameObject LicensePanel { get; }
    Button AudioSettingButton { get; }
    GameObject AudioSettingPanel { get; }
    Button StartButton { get; }
}

public interface IMainMenuManager
{
    void Initialize();
}
public class MainMenuManager : IMainMenuManager
{
    IMainMenuHolder _mainMenuHolder;
    ISceneLoader _sceneLoader;
    IAudioManager _audioManager;
    IStageSelectManager _stageSelectManager;
    
    
    public MainMenuManager(
        IMainMenuHolder mainMenuHolder,
        ISceneLoader sceneLoader,
        IAudioManager audioManager,
        IStageSelectManager stageSelectManager)
    {
        _mainMenuHolder = mainMenuHolder;
        _sceneLoader = sceneLoader;
        _audioManager = audioManager;
        _stageSelectManager = stageSelectManager;
    }
    
    public void Initialize()
    {
        _mainMenuHolder.GameEndButton.onClick.AddListener(OnGameEndButtonClicked);
        _mainMenuHolder.LicenseButton.onClick.AddListener(OnLicenseButtonClicked);
        _mainMenuHolder.AudioSettingButton.onClick.AddListener(OnAudioSettingButtonClicked);
        _mainMenuHolder.StartButton.onClick.AddListener(OnStartButtonClicked);
    }
    
    void OnGameEndButtonClicked()
    {
        _audioManager.PlaySE("ButtonPush");
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        return;
        #endif
        Application.Quit();
    }
    
    void OnLicenseButtonClicked()
    {
        _audioManager.PlaySE("ButtonPush");
        _mainMenuHolder.LicensePanel.SetActive(true);
    }

    void OnAudioSettingButtonClicked()
    {
        _audioManager.PlaySE("ButtonPush");
        _mainMenuHolder.AudioSettingPanel.SetActive(true);
    }
    
    void OnStartButtonClicked()
    {
        _audioManager.PlaySE("ButtonPush");
        _stageSelectManager.SelectStage(0);
        _sceneLoader.LoadScene(SceneType.StageScene1).Forget();
    }
}
}