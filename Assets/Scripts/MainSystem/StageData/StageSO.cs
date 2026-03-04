using MainSystem.Audio;
using MainSystem.Dialog;
using UnityEngine;

namespace MainSystem.StageData
{
[CreateAssetMenu(menuName = "ScriptableObject/StageData")]
public class StageSO : ScriptableObject
{
    [SerializeField] string title;
    public string Title => title;
    public int StageIndex;
    
    //ステージデータ全てを管轄するクラス
    [SerializeField] float stageTimeLimit;//ステージの制限時間
    public float StageTimeLimit => stageTimeLimit;
    
    [SerializeField]ItemSO itemSO;
    public ItemSO ItemSO => itemSO;

    [SerializeField] DialogSO dialogSO;
    public DialogSO DialogSO => dialogSO;
    
    [SerializeField] string dialogBGMName;
    public string DialogBGMName => dialogBGMName;
    [SerializeField] string stageBGMName;
    public string StageBGMName => stageBGMName;
    [SerializeField] float stageBgmFadeInTime;
    public float StageBgmFadeInTime => stageBgmFadeInTime;
    [SerializeField] string bgmName;
    public string BGMName => bgmName;
    [SerializeField] float bgmFadeInTime;
    public float BGMFadeInTime => bgmFadeInTime;
    
    //画像
    [SerializeField] Sprite characterImage;
    public Sprite CharacterImage => characterImage;
    [SerializeField] Sprite backgroundImage;
    public Sprite BackgroundImage => backgroundImage;
    [SerializeField] Sprite clearImage;
    public Sprite ClearImage => clearImage;
    [SerializeField] Sprite clearCharacterImage;
    public Sprite ClearCharacterImage => clearCharacterImage;

}
}