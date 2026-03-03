using System.Collections.Generic;
using MainSystem.StageData;
using VContainer;

namespace MainSystem.CoreFlow
{

public interface IStageSelectManager
{
    void SelectStage(int stageIndex);
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
    List<StageSO> _stages;
    public StageSelectManager([Key("AllStages")] List<StageSO> stages)
    {
        _stages = stages;
    }

    public void SelectStage(int stageIndex)
    {
        _selectedStageSO = _stages[stageIndex];
    }

    public StageSO GetSelectedStage()
    {
        return _selectedStageSO;
    }

    public StageSO Get => _selectedStageSO;
}
}