using System;
using System.Collections.Generic;
using MainSystem.StageData;
using UnityEngine;

namespace MainSystem.Saves
{
public interface ISavesManager
{
    List<(int index,int score)> GetStageScores();
    void Initialize(int stageCount);
    void SaveStageScore(int stageIndex,int score);
}
public class SaveManager : ISavesManager
{
    List<(int index, int score)> _stageScores = new();

    public List<(int index, int score)> GetStageScores()
    {
        return _stageScores;
    }

    bool _isInitialized = false;
    public void Initialize(int stageCount)
    {  
        if (_isInitialized)
        {
            Debug.LogError("SaveManagerは既に初期化されています。");
            return;
        }
        
        _stageScores = new List<(int index, int score)>();
        for (int i = 0; i < stageCount; i++)
        {
            _stageScores.Add((i, -1)); //初期スコアは-1（未プレイ）とする
        }
        _isInitialized = true;
    }


    public void SaveStageScore(int stageIndex, int score)
    {
        if (!_isInitialized)
        {
            Debug.LogError("SaveManagerは初期化されていません。");
            return;
        }
        
        if (stageIndex >= _stageScores.Count)
        {
            Debug.LogError($"ステージインデックス{stageIndex}は範囲外です。");
            return;
        }

        _stageScores[stageIndex] = (stageIndex, score);
    }
}
}