using System;

public class Timer
{
    public float RemainingTime { get; private set; }

    public Timer(float duration, Action onEndEvent)
    {
        RemainingTime = duration;
        OnTimerEnd += onEndEvent;
    }

    public event Action OnTimerEnd;

    public void Tick(float tickTime)
    {
        if (RemainingTime == 0f) { return; }

        RemainingTime -= tickTime;
        CheckForTimerEnd();
    }

    private void CheckForTimerEnd()
    {
        if (RemainingTime > 0f) { return; }

        RemainingTime = 0f;
        OnTimerEnd?.Invoke();
    }
}
