using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class FacilityBuildModulePresenterTest
    {

        FacilityBuildModulePresenter createFacilityBuildModulePresenter()
        {
            var buildSystem_obj = new GameObject();
            var buildSystem = buildSystem_obj.AddComponent<BuildSystem>();
            return buildSystem.facilityBuildMod;
        }
        // A Test behaves as an ordinary method
        [Test]
        public void buildFaciltiy_fishPoint()
        {
            var facilityBuildModule = createFacilityBuildModulePresenter();
            facilityBuildModule.BuildFacility(PrefabTags.fishPoint, Vector2Int.zero);
            var hasFacility = facilityBuildModule.HasFacility(Vector2Int.zero);
            Assert.IsTrue(hasFacility);
        }
        [Test]
        public void removeFacility_fishPoint()
        {
            var facilityBuildModule = createFacilityBuildModulePresenter();
            facilityBuildModule.BuildFacility(PrefabTags.fishPoint, Vector2Int.zero);
            var hasFacility_1 = facilityBuildModule.HasFacility(Vector2Int.zero);
            Assert.IsTrue(hasFacility_1);

            facilityBuildModule.RemoveFacility(Vector2Int.zero);
            var hasFacility_2 = facilityBuildModule.HasFacility(Vector2Int.zero);
            Assert.IsFalse(hasFacility_2);
        }
        [Test]
        public void onFishPointBuilt_prop_fishPoint()
        {
            var facilityBuildModule = createFacilityBuildModulePresenter();
            facilityBuildModule.OnFacilityAdded
                .Subscribe(x =>
                {
                    Debug.Log(x.Value.type);
                    Debug.Log(x.Value.position);
                    Assert.AreEqual("prop_fishPoint", x.Value.type);
                    Assert.AreEqual(Vector2Int.zero, x.Value.position);
                });

            facilityBuildModule.BuildFacility(PrefabTags.fishPoint, Vector2Int.zero);
        }
        [Test]
        public void onFishPointRemoved_prop_fishPoint()
        {
            var facilityBuildModule = createFacilityBuildModulePresenter();
            facilityBuildModule.OnFacilityRemoved
                .Subscribe(x =>
                {
                    Debug.Log(x.Value.type);
                    Assert.AreEqual(PrefabTags.fishPoint, x.Value.type);
                });

            facilityBuildModule.BuildFacility(PrefabTags.fishPoint, Vector2Int.zero);
            facilityBuildModule.RemoveFacility(Vector2Int.zero);
        }

    }
}
