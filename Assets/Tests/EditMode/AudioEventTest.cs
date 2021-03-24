using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Peixi;
using UniRx;
using UnityEngine;

namespace Tests
{
    public class AudioEventTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AudioEventTest_TwoAudioEventTrigger()
        {
            AudioEvents.OnAudioStart
                .Where(x => x == "OnMainBtnPressed")
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual("OnMainBtnPressed", x);
                });

            AudioEvents.OnAudioStart
                .Where(x => x == "OnInventoryBtnPressed")
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    Assert.AreEqual("OnInventoryBtnPressed", x);
                });

            AudioEvents.StartAudio("OnMainBtnPressed");
            AudioEvents.StartAudio("OnInventoryBtnPressed");
        }
    }
}
