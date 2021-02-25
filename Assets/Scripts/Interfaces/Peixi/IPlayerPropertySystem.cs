using System;
using UniRx;

namespace Peixi
{
    public interface IPlayerPropertySystem
    {
        /// <summary>
        /// 健康值
        /// </summary>
        int Health { get;}
        /// <summary>
        /// 饥饿值
        /// </summary>
        int Hunger { get;}
        /// <summary>
        /// 口渴值
        /// </summary>
        int Thirst { get;}
        /// <summary>
        /// 心情值（愉悦感）
        /// </summary>
        int Pleasure { get;}
        PropertyLevel HealthLevel { get; }
        PropertyLevel HungerLevel { get; }
        PropertyLevel ThirstLevel { get; }
        PropertyLevel PleasureLevel { get; }

        int ChangeHealth(int changeValue);
        int ChangeHunger(int changeValue);
        int ChangeThirst(int changeValue);
        int ChangePleasure(int changeValue);

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
        IObservable<PropertyLevel> OnHealthLevelChanged { get; }
        IObservable<PropertyLevel> OnHungerLevelChanged { get; }
        IObservable<PropertyLevel> OnThirstLevelChanged { get; }
        IObservable<PropertyLevel> OnPleasureLevelChanged { get; }

    }
}
