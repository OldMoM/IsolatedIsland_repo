using System;
using UniRx;

public interface IPlayerPropertySystem 
{
    /// <summary>
    /// 健康值
    /// </summary>
    int Health { get; set; }
    /// <summary>
    /// 饥饿值
    /// </summary>
    int Hunger { get; set; }
    /// <summary>
    /// 口渴值
    /// </summary>
    int Thirst { get; set; }
    /// <summary>
    /// 心情值（愉悦感）
    /// </summary>
    int Pleasure { get; set; }
    /// <summary>
    /// 当修改健康值时，触发此事件
    /// </summary>
    IObservable<int> OnHealthChanged { get; }
    /// <summary>
    /// 当修改饥饿值时，触发此事件
    /// </summary>
    IObservable<int> OnHungerChanged { get; }
    /// <summary>
    /// 当修改口渴值时，触发此事件
    /// </summary>
    IObservable<int> OnThirstChanged { get; }
    /// <summary>
    /// 当修改心情值时，触发此事件
    /// </summary>
    IObservable<int> OnPleasureChanged { get; }
    /// <summary>
    /// 当健康值归零时，触发此事件
    /// </summary>
    IObservable<Unit> OnPlayerDied { get; }
}
