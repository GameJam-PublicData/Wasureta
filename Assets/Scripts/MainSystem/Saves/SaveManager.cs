using System;
using System.Collections.Generic;

namespace MainSystem.Saves
{
public interface ISavesManager
{
    List<(int index,int score)> GetStageScores();
    void SaveStageScore(int stageIndex,int score);
}
public class SaveManager : ISavesManager
{
    List<(int index, int score)> _stageScores = new();

    public List<(int index, int score)> GetStageScores()
    {
        return _stageScores;
    }

    public void SaveStageScore(int stageIndex, int score)
    {
        
        
    }
}
}