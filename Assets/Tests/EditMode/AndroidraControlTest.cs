using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
using UnityEngine.AI;
namespace Tests
{
    public class AndroidraControlTest
    {
        IAndroidraSystem createFakeAndroidra()
        {
            var fakeTestObject = new GameObject();
            IAndroidraSystem iandroidraSystem = fakeTestObject.AddComponent<AndroidraSystem>();

            return iandroidraSystem;
        }
        // A Test behaves as an ordinary method
        [Test]
        public void AndroidraControl_onReceiveBuildMes_interact()
        {
            var android = createFakeAndroidra();
            var control = android.Control;

            control.OnBuildMsgReceived
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual(PrefabTags.fishPoint, x.Item1);
                    Assert.AreEqual(Vector2Int.zero, x.Item2);
                });

            control.BuildAt(PrefabTags.fishPoint, Vector2Int.zero);
        }
    }
}
