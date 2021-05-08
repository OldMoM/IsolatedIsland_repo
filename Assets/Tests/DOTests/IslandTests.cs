using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class IslandTests
    {
        private IslandModel CreateFakeIslandModel()
        {
            var model = EntityModelFactory.CreateIslandModel();
            var onDayStart = new Subject<int>();
            model.onDayStart = onDayStart;
            var onDayEnd = new Subject<int>();
            model.onDayEnd = onDayEnd;
            model.isActive = true;
            model.attachedObject = new GameObject();
            return model;
        }
        [UnityTest]
        public IEnumerator Durability_After3sec_isActive_98()
        {
            var model = CreateFakeIslandModel();
            IslandPresenterNew.Init(ref model);

            yield return new WaitForSeconds(3);
            Assert.AreEqual(98, model.durability.Value);
            yield return null;
        }
        [UnityTest]
        public IEnumerator Durability_After4sec_disabled_100()
        {
            var model = CreateFakeIslandModel();
            model.isActive = false;
            IslandPresenterNew.Init(ref model);
            yield return new WaitForSeconds(2);

            Assert.IsFalse(model.isActive);
            Assert.AreEqual(100, model.durability.Value);
            yield return null;
        }
        [Test]
        public void SetDurability_Actived_34()
        {
            var model = CreateFakeIslandModel();
            IslandPresenterNew.Init(ref model);

            model.durability.Value = 50;

            Assert.AreEqual(50, model.durability.Value);
        }
        [Test]
        public void Isactive_onDayStart_true()
        {
            var model = EntityModelFactory.CreateIslandModel();
            var onDayStart = new Subject<int>();
            model.onDayStart = onDayStart;
            model.onDayEnd = new Subject<int>();
            model.isActive = false;
            model.attachedObject = new GameObject();

            IslandPresenterNew.Init(ref model);

            onDayStart.OnNext(3);

            Assert.IsTrue(model.isActive);
        }

        [Test]
        public void IsActive_onDayEnd_false()
        {
            var model = EntityModelFactory.CreateIslandModel();
            var onDayStart = new Subject<int>();
            model.onDayStart = onDayStart;
            var onDayEnd = new Subject<int>();
            model.onDayEnd = onDayEnd;
            model.isActive = true;
            model.attachedObject = new GameObject();

            IslandPresenterNew.Init(ref model);
            onDayEnd.OnNext(4);

            Assert.IsFalse(model.isActive);

        }
        [Test]
        public void OnDestoryed_Get0dot0()
        {
            var model = CreateFakeIslandModel();
            IslandPresenterNew.Init(ref model);

            model.onIslandDestoryed
                .Subscribe(x =>
                {
                    Debug.Log("island destoryed");
                    Assert.AreEqual(Vector2Int.zero, x);
                });

            model.durability.Value = 0;
        }
    }
}
