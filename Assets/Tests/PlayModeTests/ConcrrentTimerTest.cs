using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class ConcrrentTimerTest
    {

        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ConcrrentTimer_OnTimerStart_Unit()
        {
            ConcurrentTimer timer = new ConcurrentTimer();
            timer.OnTimerStart
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(Unit.Default, x);
                });

            timer.StartTimeCountdown(1);
            yield return new WaitForSeconds(1.5f);
        }
        [UnityTest]
        public IEnumerator ConcrrentTimer_OnTimerEnd_Unit()
        {
            ConcurrentTimer timer = new ConcurrentTimer();
            timer.OnTimerEnd
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(Unit.Default, x);
                });
            timer.StartTimeCountdown(1);
            yield return new WaitForSeconds(1.5f);
                
        }
        [UnityTest]
        public IEnumerator ConcurrentTimer_true()
        {
            ConcurrentTimer timer = new ConcurrentTimer();
            timer.OnProcessChanged
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.IsTrue(x >= 0);
                });
            timer.StartTimeCountdown(0.2f);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
