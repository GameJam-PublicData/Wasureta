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
    
    //ステージデータ全てを管轄するクラス
    [SerializeField] float stageTimeLimit;//ステージの制限時間
    public float StageTimeLimit => stageTimeLimit;
    
    [SerializeField]ItemSO itemSO;
    public ItemSO ItemSO => itemSO;

    [SerializeField] DialogSO dialogSO;
    public DialogSO DialogSO => dialogSO;
    
    [SerializeField] string bgmName;
    public string BGMName => bgmName;
    [SerializeField] float bgmFadeInTime;
    public float BGMFadeInTime => bgmFadeInTime;
}
}