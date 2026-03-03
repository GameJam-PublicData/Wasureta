using Cysharp.Threading.Tasks;
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
    IStageSelectManager _stageSelectManager;
    
    
    public MainMenuManager(
        IMainMenuHolder mainMenuHolder
        ,ISceneLoader sceneLoader,
        IStageSelectManager stageSelectManager)
    {
        _mainMenuHolder = mainMenuHolder;
        _sceneLoader = sceneLoader;
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
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        return;
        #endif
        Application.Quit();
    }
    
    void OnLicenseButtonClicked()
    {
        _mainMenuHolder.LicensePanel.SetActive(true);
    }

    void OnAudioSettingButtonClicked()
    {
        _mainMenuHolder.AudioSettingPanel.SetActive(true);
    }
    
    void OnStartButtonClicked()
    {
        _stageSelectManager.SelectStage(0);
        _sceneLoader.LoadScene(SceneType.StageScene).Forget();
    }
}
}