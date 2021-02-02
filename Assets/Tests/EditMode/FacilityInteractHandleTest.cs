using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
namespace Tests
{
    public class FacilityInteractHandleTest
    {
        [Test]
        public void onPlayerTouchFacility_1()
        {
            var handle = new FacilityInteractionAgent();

            FacilityData facility1 = new FacilityData();
            facility1.instanceId = 1;
            facility1.name = "fish";
            facility1.position = Vector3.zero;

            handle.PlayerTouchFacility(facility1);

            var targetId = handle.targetData.instanceId;
            Assert.AreEqual(1, targetId);
        }
        [Test]
        public void onPlayerTouchFacilitys_2()
        {
            var handle = new FacilityInteractionAgent();

            FacilityData facility1 = new FacilityData();
            FacilityData facility2 = new FacilityData();
            facility1.instanceId = 1;
            facility1.name = "fish";
            facility1.position = Vector3.zero;

            facility2.instanceId = 2;
            facility2.name = "fish";
            facility2.position = Vector3.zero;

            handle.PlayerTouchFacility(facility1);
            handle.PlayerTouchFacility(facility2);

            var targetId = handle.targetData.instanceId;
            Assert.AreEqual(2, targetId);
        }
        [Test]
        public void switchTarget_2()
        {
            var handle = new FacilityInteractionAgent();
            FacilityData facility1 = new FacilityData();
            facility1.instanceId = 1;
            facility1.name = "fish";
            facility1.position = Vector3.zero;

            FacilityData facility2 = new FacilityData();
            facility2.instanceId = 2;
            facility2.name = "fish";
            facility2.position = Vector3.zero;

            FacilityData facility3 = new FacilityData();
            facility3.instanceId = 3;
            facility3.name = "fish";
            facility3.position = Vector3.zero;

            handle.PlayerTouchFacility(facility1);
            handle.PlayerTouchFacility(facility2);
            handle.PlayerTouchFacility(facility3);

            Assert.AreEqual(3, handle.targetData.instanceId);
            handle.SwitchTarget();
            Assert.AreEqual(2, handle.targetData.instanceId);
        }
        [Test]
        public void onTargetChanged_onPlayerTouched_1()
        {
            var handle = new FacilityInteractionAgent();

            FacilityData facility1 = new FacilityData();
            facility1.instanceId = 1;
            facility1.name = "fish";
            facility1.position = Vector3.zero;

            handle.onTargetChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Assert.AreEqual(1, x.instanceId);
                });

            handle.PlayerTouchFacility(facility1);
        }
        [Test]
        public void onTargetChanged_onSwitchTarget_1()
        {
            var handle = new FacilityInteractionAgent();

            FacilityData facility1 = new FacilityData();
            FacilityData facility2 = new FacilityData();
            facility1.instanceId = 1;
            facility1.name = "fish";
            facility1.position = Vector3.zero;

            facility2.instanceId = 2;
            facility2.name = "fish";
            facility2.position = Vector3.zero;

            handle.onTargetChanged
                .Skip(3)
                .Subscribe(x =>
                {
                    Assert.AreEqual(1, x.instanceId);
                });
            handle.PlayerTouchFacility(facility1);
            handle.PlayerTouchFacility(facility2);
            handle.SwitchTarget();
        }
        [Test]
        public void onPlayerUntouch_left_1()
        {
            var handle = new FacilityInteractionAgent();
            FacilityData facility1 = new FacilityData();
            FacilityData facility2 = new FacilityData();
            facility1.instanceId = 1;
            facility2.instanceId = 2;

            handle.onTargetChanged
                .Skip(3)
                .Subscribe(x =>
                {
                    Assert.AreEqual(1, x.instanceId);
                });

            handle.PlayerTouchFacility(facility1);
            handle.PlayerTouchFacility(facility2);
            handle.PlayerUntouchFacility(facility2);

        }
        [Test]
        public void onPlayerTouch_Contact()
        {
            var handle = new FacilityInteractionAgent();
            FacilityData facility1 = new FacilityData();

            handle.onStateChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Assert.AreEqual(InteractState.Contact, x);
                });

            handle.PlayerTouchFacility(facility1);
        }
        [Test]
        public void onPlayerUntouch_Idle()
        {
            var handle = new FacilityInteractionAgent();
            FacilityData facility1 = new FacilityData();

            handle.onStateChanged
                .Skip(2)
                .Subscribe(x =>
                {
                    Assert.AreEqual(InteractState.Idle, x);
                });

            handle.PlayerTouchFacility(facility1);
            handle.PlayerUntouchFacility(facility1);
        }
        [Test]
        public void startFishPointInteract_Interact()
        {
            var handle = new FacilityInteractionAgent();
            handle.onStateChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    Assert.AreEqual(InteractState.Interact, x);
                });
            handle.InteractStart(FacilityType.FishPoint);
        }
        [Test]
        public void endFishPointInteract_Idle()
        {
            var handle = new FacilityInteractionAgent();
            handle.onStateChanged
                .Skip(2)
                .Subscribe(x =>
                {
                    Assert.AreEqual(InteractState.Idle, x);
                });

            handle.InteractStart(FacilityType.FishPoint);
            handle.InteractEnd(FacilityType.FishPoint);
        }
    }
}
