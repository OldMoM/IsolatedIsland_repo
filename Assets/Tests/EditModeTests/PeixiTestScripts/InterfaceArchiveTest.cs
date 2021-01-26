using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using Siwei;
using System;
using UniRx;

namespace Tests
{
    public class InterfaceArchiveTest
    {
        [Test]
        public void GetIbuildSystem_systemNotInHierarchy_null()
        {
            Assert.IsNull(InterfaceArichives.Archive.IbuildSystem);
        }
        [Test]
        public void GetIbuildSystem_systemInHirerchy_notNull()
        {
            var t = new GameObject();
            t.AddComponent<BuildSystem>();
            var ibuildSystem = InterfaceArichives.Archive.IbuildSystem;
            Assert.IsNotNull(ibuildSystem);
            GameObject.DestroyImmediate(t);
        }
        [Test]
        public void GetIplayerPropetySystem_systemNotInHierarchy_null()
        {
            Assert.IsNull(InterfaceArichives.Archive.playerPropertySystem);
        }
        [Test]
        public void GetIPlayerPropertySystem_systemInHirer_notNull()
        {
            var t = new GameObject();
            var fakeInterface = t.AddComponent<FakePlayerPropertySystem>();
            InterfaceArichives.Archive.playerPropertySystem = fakeInterface;
            Assert.IsNotNull(InterfaceArichives.Archive.playerPropertySystem);
            GameObject.DestroyImmediate(t);
        }
    }
    public class FakePlayerPropertySystem : MonoBehaviour,IPlayerPropertySystem
    {
        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Hunger { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Thirst { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Pleasure { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IObservable<int> OnHealthChanged => throw new NotImplementedException();

        public IObservable<int> OnHungerChanged => throw new NotImplementedException();

        public IObservable<int> OnThirstChanged => throw new NotImplementedException();

        public IObservable<int> OnPleasureChanged => throw new NotImplementedException();

        public IObservable<Unit> OnPlayerDied => throw new NotImplementedException();
    }

}
