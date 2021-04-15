using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class TimeSystemTest
    {
        [UnityTest]
        public IEnumerator OnDayStart_GameStart_ReceiveInt1()
        {
            var timeSystem = new GameObject().AddComponent<TimeSystem>();

            timeSystem.onDayStart
                .Subscribe(x =>
                {
                    
                    Debug.Log("start first day");
                    Assert.AreEqual(1, x);
                });

            yield return null;
        }
    }
}
