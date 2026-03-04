using TMPro;
using UnityEngine;
using VContainer;

namespace StageSystem.Timer
{
public class TimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] RectTransform clockHand;
    
    ITimeManager _timeManager;
    float _totalTime; // 制限時間の総量（初期値）
    bool _isInitialized;
    
    [Inject]
    public void Construct(ITimeManager timeManager)
    {
        _timeManager = timeManager;
    }
    
    void Update()
    {
        if (_timeManager == null) return;
        
        // StartTimerが呼ばれて残り時間が設定されたタイミングで総時間を記録
        if (!_isInitialized)
        {
            float remaining = (float)_timeManager.GetRemainingTime();
            if (remaining <= 0f) return; // まだStartTimerが呼ばれていない
            _totalTime = remaining;
            _isInitialized = true;
        }
        
        double remainingTime = _timeManager.GetRemainingTime();
        
        // TMPに残り時間を表示
        timerText.text = $"残り時間: {remainingTime:F2}";
        
        // 残り時間の割合に応じて時計の針を回転（360° → 0°）
        float ratio = Mathf.Clamp01((float)(remainingTime / _totalTime));
        float angle = ratio * 360f;
        clockHand.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
}
  
  
