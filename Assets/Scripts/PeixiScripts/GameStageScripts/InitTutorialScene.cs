using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

namespace Peixi
{
    public class InitTutorialScene : MonoBehaviour
    {
        public List<Vector2Int> startIslandSet;
        public Transform player;

        private void Start()
        {
            InitScene();
        }

        public void InitScene()
        {
            var iBuildSystem = InterfaceArichives.Archive.IBuildSystem;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    startIslandSet.Add(new Vector2Int(i, j));
                }
            }

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
