using TMPro;
using UnityEngine;
using VContainer;

namespace StageSystem.Timer
{
public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    ITimeManager _timeManager;
    [Inject]
    public void Construct(ITimeManager timeManager)
    {
        _timeManager = timeManager;
    }
    
    void Update()
    {
        if (_timeManager != null)
        {
            double elapsedTime = _timeManager.GetRemainingTime();
            timerText.text = $"RemainingTime: {elapsedTime:F2} ";
        }
    }
}
}
  
  
