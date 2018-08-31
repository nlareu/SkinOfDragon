using UnityEngine;

public class TimeInterval
{
    public float Value { get; private set; }
    public float Interval { get; private set; }

    public TimeInterval(float interval)
    {
        this.Interval = interval;
    }

    public float Accumulate()
    {
        this.Value += Time.deltaTime;

        return this.Value;
    }
}