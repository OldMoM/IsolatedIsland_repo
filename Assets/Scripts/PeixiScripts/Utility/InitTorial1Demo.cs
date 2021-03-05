using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class InitTorial1Demo : MonoBehaviour
    {
        public Vector2Int[] startIslandSet;
        public Transform player;
        public GameObject androidra;

        private void Start()
        {
            var iBuildSystem = InterfaceArichives.Archive.IBuildSystem;

            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(x =>
                {
                    foreach (var islandPos in startIslandSet)
                    {
                        iBuildSystem.BuildIslandAt(islandPos);
                    }

                    iBuildSystem.BuildFacility(new Vector2Int(1, 1), PrefabTags.foodPlant);
                    iBuildSystem.BuildFacility(new Vector2Int(0, -1), PrefabTags.waterPuifier);
                    iBuildSystem.BuildFacility(new Vector2Int(-1, -1), PrefabTags.foodPlant);

                    
                });

        }
    }
}
