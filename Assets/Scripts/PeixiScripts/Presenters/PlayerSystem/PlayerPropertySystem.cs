using System;
using UniRx;
using UnityEngine;

namespace Peixi
{
    [Serializable]
    public class PlayerPropertySystem : IPlayerPropertySystem
    {
        public PlayerPropertySystemModel property;
        public int Health => property.health.Value;
        public int Satiety => property.hunger.Value;
        public int Thirst => property.thirst.Value;
        public int Pleasure => property.pleasure.Value;
        public PropertyLevel HealthLevel
        {
            get => property.healthLevel.Value;
            set
            {
                property.healthLevel.Value = value;
            }
        }
        public PropertyLevel SatietyLevel
        {
            get => property.hungerLevel.Value;
            set
            {
                property.hungerLevel.Value = value;
            }
        }
        public PropertyLevel ThirstLevel
        {
            get => property.thirstLevel.Value;
            set
            {
                property.thirstLevel.Value = value;
            }
        }
        public PropertyLevel PleasureLevel
        {
            get => property.pleasureLevel.Value;
            set
            {
                property.pleasureLevel.Value = value;
            }
        }
        public IObservable<int> OnHealthChanged => property.health;
        public IObservable<int> OnSatietyChanged => property.hunger;
        public IObservable<int> OnThirstChanged => property.thirst;
        public IObservable<int> OnPleasureChanged => property.pleasure;
        public IObservable<Unit> OnPlayerDied => property.onPlayerDied;
        public IObservable<PropertyLevel> OnHealthLevelChanged => property.healthLevel;
        public IObservable<PropertyLevel> OnSatietyLevelChanged => property.hungerLevel;
        public IObservable<PropertyLevel> OnThirstLevelChanged => property.thirstLevel;
        public IObservable<PropertyLevel> OnPleasureLevelChanged => property.pleasureLevel;
        public int MaxHealth
        {
            get => property.maxHealth;
            set
            {
                property.maxHealth = value;
                var currentHealth = Health;
                if (currentHealth >= property.maxHealth)
                {
                    property.health.Value = MaxHealth;
                }
            }
        }

        public int ChangeHealth(int changeValue)
        {
            var tempHealth = Health;
            tempHealth += changeValue;
            tempHealth = Mathf.Clamp(tempHealth, 0, property.maxHealth);
            property.health.Value = tempHealth;

            if (tempHealth <= 0 )
            {
                property.isDied = true;
                property.onPlayerDied.OnNext(Unit.Default);
            }
            return tempHealth;
        }
        public int ChangeSatiety(int changeValue)
        {
            var tempHunger = Satiety;
            tempHunger += changeValue;
            tempHunger = Mathf.Clamp(tempHunger, 0, 100);
            property.hunger.Value = tempHunger;
            return tempHunger;
        }
        public int ChangePleasure(int changeValue)
        {
            var tempPleasure = Pleasure;
            tempPleasure += changeValue;
            tempPleasure = Mathf.Clamp(tempPleasure, 0, 100);
            property.pleasure.Value = tempPleasure;
            return tempPleasure;
        }
        public int ChangeThirst(int changeValue)
        {
            var tempThirst = Thirst;
            tempThirst += changeValue;
            tempThirst = Mathf.Clamp(tempThirst, 0, 100);
            property.thirst.Value = tempThirst;
            return tempThirst;
        }
        public PlayerPropertySystem()
        {
            Config();
        }
        PlayerPropertySystem Config()
        {
            property.health = new IntReactiveProperty(50);
            property.hunger = new IntReactiveProperty(60);
            property.thirst = new IntReactiveProperty(80);
            property.pleasure = new IntReactiveProperty(40);

            property.onPlayerDied = new Subject<Unit>();

            property.healthLevel = new ReactiveProperty<PropertyLevel>(PropertyLevel.Safe);
            property.hungerLevel = new ReactiveProperty<PropertyLevel>(PropertyLevel.Safe);
            property.thirstLevel = new ReactiveProperty<PropertyLevel>(PropertyLevel.Safe);
            property.pleasureLevel = new ReactiveProperty<PropertyLevel>(PropertyLevel.Safe);

            property.maxHealth = 100;

            return this;
        }
        PlayerPropertySystem React(Action action)
        {
            action();
            return this;
        }
    }
    [Serializable]
    public struct PlayerPropertySystemModel
    {
        public IntReactiveProperty health;
        public IntReactiveProperty hunger;
        public IntReactiveProperty thirst;
        public IntReactiveProperty pleasure;
        public bool isDied;
        public Subject<Unit> onPlayerDied;
        public ReactiveProperty<PropertyLevel> healthLevel;
        public ReactiveProperty<PropertyLevel> hungerLevel;
        public ReactiveProperty<PropertyLevel> thirstLevel;
        public ReactiveProperty<PropertyLevel> pleasureLevel;
        public int maxHealth;

        /// <summary>
        /// It's safe while the evalueated property at 71~100
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public PropertyLevel NegativeEvaluate(int property)
        {
            if (property>=67 && property<=100)
            {
                return PropertyLevel.Safe;
            }
            else if (property >=31 && property<= 66)
            {
                return PropertyLevel.Euclid;
            }
            return PropertyLevel.Keter;
        }
        /// <summary>
        /// It's safe while the evalueated property at 0~30
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public PropertyLevel PositiveEvaluate(int property)
        {
            if (property >= 67 && property <= 100)
            {
                return PropertyLevel.Keter;
            }
            else if (property >= 31 && property <= 66)
            {
                return PropertyLevel.Euclid;
            }
            return PropertyLevel.Safe;
        }
    }
    public enum PropertyLevel
    {
        Safe,
        Euclid,
        Keter
    }
}
