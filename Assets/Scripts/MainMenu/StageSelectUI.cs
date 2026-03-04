using DG.Tweening;
using MainSystem.Audio;
using MainSystem.CoreFlow;
using MainSystem.Scene;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MainMenu
{
public class StageSelectUI : MonoBehaviour
{
    [SerializeField] Button gameStartButton;
    [SerializeField] Button stageSelectExitButton;

    [SerializeField] Button stageButton1; 
    [SerializeField] Button stageButton2;
    [SerializeField] Button stageButton3;

    [SerializeField] RectTransform NomalHUD;
    [SerializeField] RectTransform StageSelectHUD;

    bool _isAnimation = false;
    
    IAudioManager _audioManager;
    ISceneLoader _sceneLoader;
    IStageSelectManager _stageSelectManager;

    [Inject]
    public void Construct(
        IAudioManager audioManager, 
        ISceneLoader sceneLoader,
        IStageSelectManager stageSelectManager)
    {
        _audioManager = audioManager;
        _sceneLoader = sceneLoader;
        _stageSelectManager = stageSelectManager;
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //関数登録
        gameStartButton.onClick.AddListener(GameStartButtonClicked);
        stageSelectExitButton.onClick.AddListener(StageSelectExitButtonClicked);
        stageButton1.onClick.AddListener(StageButton1Clicked);
        stageButton2.onClick.AddListener(StageButton2Clicked);
        stageButton3.onClick.AddListener(StageButton3Clicked);
    }

    void GameStartButtonClicked()
    {
        _audioManager.PlaySE("ButtonPush");
        
        if(_isAnimation){return;}
        
        //アニメーション
        _isAnimation = true;
        NomalHUD.anchoredPosition = Vector2.zero;
        StageSelectHUD.anchoredPosition = new Vector2(2000f,-50f);

        NomalHUD.DOAnchorPosX(-2000f, 0.5f).SetEase(Ease.InOutSine);
        StageSelectHUD.DOAnchorPosX(0f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => _isAnimation = false);
    }

    void StageSelectExitButtonClicked()
    {
        _audioManager.PlaySE("ButtonPush");
        
        if(_isAnimation){return;}
        
        //アニメーション
        _isAnimation = true;
        NomalHUD.anchoredPosition = new Vector2(-2000f,0f);
        StageSelectHUD.anchoredPosition = new Vector2(0f,-50f);
        
        NomalHUD.DOAnchorPosX(0f, 0.5f).SetEase(Ease.InOutSine);
        StageSelectHUD.DOAnchorPosX(2000f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => _isAnimation = false);
        
        
    }

    void StageButton1Clicked()
    {
        _audioManager.PlaySE("ButtonPush");
        
        _stageSelectManager.SelectStage(0);
        _sceneLoader.LoadScene(SceneType.DialogScene);
    }

    void StageButton2Clicked()
    {
        _audioManager.PlaySE("ButtonPush");
        
        _stageSelectManager.SelectStage(1);
        _sceneLoader.LoadScene(SceneType.DialogScene);
    }

    void StageButton3Clicked()
    {
        _audioManager.PlaySE("ButtonPush");
        
        _stageSelectManager.SelectStage(2);
        _sceneLoader.LoadScene(SceneType.DialogScene);
    }
}
}
