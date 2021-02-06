using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
namespace Tests
{
    public class PlayerPropertyTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ChangeHealth_95()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.ChangeHealth(-5);
            Assert.AreEqual(95, property.Health);
        }
        [Test]
        public void OnHealthChanged_93()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.OnHealthChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(93, x);
                });
            property.ChangeHealth(-7);
        }
        [Test]
        public void OnPlayerDied_true()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.OnPlayerDied
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(Unit.Default, x);
                });
            property.ChangeHealth(-150);
        }
        [Test]
        public void ChangeHunger_12()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.ChangeHunger(2);
            Assert.AreEqual(12, property.Hunger);

        }
        [Test]
        public void OnHungerChanged_12()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.OnHungerChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(12, x);
                });
            property.ChangeHealth(2);
        }
        [Test]
        public void ChangeThirst_13()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.ChangeThirst(3);
            Assert.AreEqual(13, property.Thirst);
        }
        [Test]
        public void OnThirstChanged_13()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.OnThirstChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(13, x);
                });
            property.ChangeThirst(3);
        }
        [Test]
        public void ChangePleasure_15()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.ChangePleasure(5);
            Assert.AreEqual(15, property.Pleasure);

        }
        [Test]
        public void OnPleasureChanged_15()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.OnPleasureChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(15, x);
                });
            property.ChangePleasure(5);
        }
    }
}
