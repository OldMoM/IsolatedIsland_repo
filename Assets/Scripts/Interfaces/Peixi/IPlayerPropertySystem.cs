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
        /// 饱腹感
        /// </summary>
        int Satiety { get;}
        /// <summary>
        /// 口渴值
        /// </summary>
        int Thirst { get;}
        /// <summary>
        /// 心情值（愉悦感）
        /// </summary>
        int Pleasure { get;}
        /// <summary>
        /// 健康等级
        /// </summary>
        PropertyLevel HealthLevel { get; set; }
        /// <summary>
        /// 饱腹等级
        /// </summary>
        PropertyLevel SatietyLevel { get; set; }
        /// <summary>
        /// 口渴等级
        /// </summary>
        PropertyLevel ThirstLevel { get; set; }
        /// <summary>
        /// 心情等级
        /// </summary>
        PropertyLevel PleasureLevel { get; set; }
        int ChangeHealth(int changeValue);
        int ChangeSatiety(int changeValue);
        int ChangeThirst(int changeValue);
        int ChangePleasure(int changeValue);
        /// <summary>
        /// 当修改健康值时，触发此事件
        /// </summary>
        IObservable<int> OnHealthChanged { get; }
        /// <summary>
        /// 当修改饱腹值时，触发此事件
        /// </summary>
        IObservable<int> OnSatietyChanged { get; }
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
        IObservable<PropertyLevel> OnSatietyLevelChanged { get; }
        IObservable<PropertyLevel> OnThirstLevelChanged { get; }
        IObservable<PropertyLevel> OnPleasureLevelChanged { get; }
    }
}
