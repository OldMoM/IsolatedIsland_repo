using UniRx;
using System;

[Obsolete("Please replace it with Peixi.ITimeSystem")]
public interface ITimeSystemData 
{
    float DayCount { get; }
    float TimeCountdown { get;}
    bool IsDay { get; }
}
