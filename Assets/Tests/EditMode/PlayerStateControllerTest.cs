using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class PlayerStateControllerTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void OnInteractStart_Interact()
        {
            var facilityInteractAgent = new FacilityInteractionAgent();
            var playerStateController = new PlayerStateController();

            facilityInteractAgent.onStateChanged
                .Where(x => x.Equals(InteractState.Interact))
                .Subscribe(x =>
                {
                    Assert.AreEqual(InteractState.Interact, x);
                    Debug.Log(x);
                });
            facilityInteractAgent.fishUnit.startInteract();    
        }
    }
}
