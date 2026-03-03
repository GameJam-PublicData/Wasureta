using MainSystem.StageData;

namespace MainSystem.CoreFlow
{

public interface IStageSelectManager
{
    void SelectStage(StageSO stageSO);
    StageSO GetSelectedStage();
}
public interface IStageSOProvider
{
    StageSO Get { get; }
}
//現在どのステージを選択しているかを管理
public class StageSelectManager : IStageSelectManager, IStageSOProvider
{
    StageSO _selectedStageSO;

    public void SelectStage(StageSO stageSO)
    {
        _selectedStageSO = stageSO;
    }

    public StageSO GetSelectedStage()
    {
        return _selectedStageSO;
    }

    public StageSO Get => _selectedStageSO;
}
}