using System;
using UniRx;
using UnityEngine;

namespace Peixi
{
    [Serializable]
    public class PlayerPropertySystem : IPlayerPropertySystem
    {
        public PlayerPropertySystemModel property;
        public int Health { get => property.health.Value; }
        public int Hunger { get => property.hunger.Value;}
        public int Thirst { get => property.thirst.Value; }
        public int Pleasure { get => property.pleasure.Value; }
        public IObservable<int> OnHealthChanged => property.health;
        public IObservable<int> OnHungerChanged => property.hunger;
        public IObservable<int> OnThirstChanged => property.thirst;
        public IObservable<int> OnPleasureChanged => property.pleasure;
        public IObservable<Unit> OnPlayerDied => property.onPlayerDied;
        public int ChangeHealth(int changeValue)
        {
            var tempHealth = Health;
            tempHealth += changeValue;
            tempHealth = Mathf.Clamp(tempHealth, 0, 100);
            property.health.Value = tempHealth;

            if (tempHealth <= 0 )
            {
                property.isDied = true;
                property.onPlayerDied.OnNext(Unit.Default);
            }
            return tempHealth;
        }
        public int ChangeHunger(int changeValue)
        {
            var tempHunger = Hunger;
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
            property.health = new IntReactiveProperty(100);
            property.hunger = new IntReactiveProperty(10);
            property.thirst = new IntReactiveProperty(10);
            property.pleasure = new IntReactiveProperty(10);
            property.onPlayerDied = new Subject<Unit>();
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
    }
}
