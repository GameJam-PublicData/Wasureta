using System;
using System.Collections.Generic;
using MainSystem.StageData;
using UnityEngine;
using VContainer;

namespace MainSystem.CoreFlow
{
public class StageManager : MonoBehaviour
{
    [SerializeField] List<GameObject> stages;
    
    int _index = 0;

    [Inject]
    public void Construct(IStageSOProvider stageSOProvider)
    {
        _index = stageSOProvider.Get.StageIndex;
    }

    void Awake()
    {
        stages[_index].SetActive(true);
    }
}
}
  
  
