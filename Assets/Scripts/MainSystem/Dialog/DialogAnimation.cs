using MainSystem.CoreFlow;
using MainSystem.StageData;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace MainSystem.Dialog
{
public class DialogAnimation : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] GameObject characterObject;
    [SerializeField] Image backGroundImage;

    IStageSOProvider _stageSOProvider;
    
    StageSO _stageSO;

    [Inject]
    public void Construct(IStageSOProvider stageSOProvider)
    {
        _stageSOProvider = stageSOProvider;
    }

    void Start()
    {
        _stageSO = _stageSOProvider.Get;
        backGroundImage.sprite = _stageSO.BackgroundImage;
        characterImage.sprite = _stageSO.CharacterImage;
    }
}
}
