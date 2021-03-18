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
            property.ChangeSatiety(2);
            Assert.AreEqual(12, property.Satiety);

        }
        [Test]
        public void OnHungerChanged_58()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            var hunger = property.Satiety;

            property.OnSatietyChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log("当前生命值为: " + x);
                    Assert.AreEqual(hunger - 2, x);
                });
            property.ChangeSatiety(-2);
        }
        [Test]
        public void ChangeThirst_83()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.ChangeThirst(3);
            Assert.AreEqual(83, property.Thirst);
        }
        [Test]
        public void OnThirstChanged_83()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.OnThirstChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(83, x);
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
        [Test]
        public void PlayerPropertyTest_SetHungerLevel_Euclid()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.SatietyLevel = PropertyLevel.Euclid;
            Assert.AreEqual(PropertyLevel.Euclid, property.SatietyLevel);
        }
        [Test]
        public void PlayerPropertyTest_SetHealthLevel_Euclid()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.HealthLevel = PropertyLevel.Euclid;
            Assert.AreEqual(PropertyLevel.Euclid, property.HealthLevel);
        }
        [Test]
        public void PlayerPropertyTest_SetThirstLevel_Euclid()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.ThirstLevel = PropertyLevel.Euclid;
            Assert.AreEqual(PropertyLevel.Euclid, property.ThirstLevel);
        }
        [Test]
        public void PlayerPropertyTest_SetPleasureLevel_Euclid()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.PleasureLevel = PropertyLevel.Euclid;
            Assert.AreEqual(PropertyLevel.Euclid, property.PleasureLevel);
        }
        [Test]
        public void PlayerPropertyTest_SetHealth_120()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.MaxHealth = 120;
            Assert.AreEqual(120, property.MaxHealth);
        }
        [Test]
        public void PlayerPropertyTest_SetMaxHealth80_Health80()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            property.MaxHealth = 80;
            Assert.AreEqual(80, property.Health);
        }
        [Test]
        public void PlayerPropertyTest_SetMaxHealth25WhenHealthIs100_Health25()
        {
            PlayerPropertySystem property = new PlayerPropertySystem();
            Assert.AreEqual(100, property.Health);
            property.MaxHealth = 25;
            Assert.AreEqual(25, property.Health);
        }
    }
}
