using System;
using System.Diagnostics;
using System.Threading;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;
namespace StageSystem.Timer
{
public interface ITimeManager
{
    UniTask StartTimer(float timeLimit, CancellationToken token);
    void StopTimer();
    void ReStartTimer();
    double GetElapsedTime();
    
    
}
public class TimeManager : ITimeManager
{
    Stopwatch _stopwatch = new();
    public async UniTask StartTimer(float timeLimit, CancellationToken token)
    {
        _stopwatch.Start();
        while (token.IsCancellationRequested == false)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(timeLimit), cancellationToken: token); 
                await UniTask.WaitWhile(() => _stopwatch.IsRunning, cancellationToken: token);
            }
            finally
            {
                _stopwatch.Stop();
            }
            
            Debug.Log($"タイマー終了。経過時間: {timeLimit}秒");
            break;
        }
    }

    public void StopTimer()
    {
        _stopwatch.Stop();
    }

    public void ReStartTimer()
    {
        _stopwatch.Restart();
        
    }
    
    public double GetElapsedTime()
    {
        return (float)_stopwatch.Elapsed.TotalMilliseconds;
    }
}
}