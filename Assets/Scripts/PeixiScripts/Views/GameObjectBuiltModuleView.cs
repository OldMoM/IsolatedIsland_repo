using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    public class GameObjectBuiltModuleView : MonoBehaviour
    {
        IslandGridModulePresenter grid;
        WallGridModulePresenter wall;
        FacilityModulePresenter facility;
        IBuildSystem system;
        public GameObject[] propSampleRepo;

        public int gridSize = 5;
        public Vector3 gridOrigialPos = Vector3.zero;
        public Vector3 gridOffset = Vector3.zero;

        protected Dictionary<Vector2Int, GameObject> islandSquares = new Dictionary<Vector2Int, GameObject>();
        protected Dictionary<Vector2Int, GameObject> wallCubes = new Dictionary<Vector2Int, GameObject>();
        private void OnEnable()
        {
            system = GetComponent<IBuildSystem>();
            Assert.IsNotNull(system, "system was null at GameObjectBuiltModuleView.cs");
            grid = GetComponentInChildren<IslandGridModulePresenter>();
            wall = GetComponentInChildren<WallGridModulePresenter>();
            facility = GetComponentInChildren<FacilityModulePresenter>();

            
        }
        private void Start()
        {
            system.OnIslandBuilt
                .Subscribe(x =>
                {
                    CreateIslandCube(x.Key);
                });

            grid.OnIslandRemoved
                .Subscribe(x =>
                {
                    RemoveIslandAt(x.Key);
                });

            wall.OnWallAdded.Subscribe(x =>
            {
                CreateWallCube(x.Key);
            });

            wall.OnWallRemoved
                .Subscribe(x =>
                {
                    RemoveWallCube(x.Key);
                });

            facility.OnFacilityAdded.Subscribe(OnFacilityAdded);

            system.OnIslandSunk
                .Subscribe(RemoveIslandAt);
        }
        protected void CreateIslandCube(Vector2Int gridPos)
        {
            var islandSet = GameObject.Find("IslandSet");
            Assert.IsNotNull(islandSet);
            var position = new Vector3(gridPos.x * gridSize, 0, gridPos.y * gridSize);
            var _island = GameObject.Instantiate(propSampleRepo[0], position, Quaternion.identity, islandSet.transform);
            _island.transform.name = "IslandAt" + gridPos.x + "dot" + gridPos.y;

            var islandPresenter = _island.GetComponent<IslandPresenter>();
            Assert.IsNotNull(islandPresenter);
            islandPresenter.Active(gridPos, 100);

            islandSquares.Add(gridPos, _island);
        }
        protected void RemoveWallCube(Vector2Int gridPos)
        {
            Destroy(wallCubes[gridPos]);
            wallCubes.Remove(gridPos);
        }
        protected void CreateWallCube(Vector2Int gridPos)
        {
            var redCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            redCube.transform.localScale = new Vector3(5, 2, 5);
            redCube.transform.position = new Vector3(gridPos.x * 5, 1, gridPos.y * 5);
            var renderer = redCube.GetComponent<MeshRenderer>();
            renderer.material.color = Color.red;

            var wallSet = GameObject.Find("WallSet");
            redCube.transform.parent = wallSet.transform;
            wallCubes.Add(gridPos, redCube);
        }
        protected void RemoveIslandAt(Vector2Int pos)
        {
            GameObject.Destroy(islandSquares[pos]);
            islandSquares.Remove(pos);                                       
        }
        protected void CreateFacility(Vector2Int gridPos)
        {
            var blueCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            blueCube.transform.localScale = new Vector3(2, 4, 2);
            blueCube.transform.position = new Vector3(gridPos.x * 5, 2.5f, gridPos.y * 5);
            var renderer = blueCube.GetComponent<MeshRenderer>();
            renderer.material.color = new Color(0, 139 / 255.0f, 139 / 255f);

            wallCubes.Add(gridPos, blueCube);
        }
        private void OnFacilityAdded(DictionaryAddEvent<Vector2Int,FacilityGridData> data)
        {
            CreateFacility(data.Key);
        }
        public IIsland GetIslandInterface(Vector2Int islandGridPos)
        {
            //print("msg has ben delivered to GameObjectBuiltModuleView");
            //print("get the island interface at " + islandGridPos);
            var hasIsland = islandSquares.ContainsKey(islandGridPos);
            Assert.IsTrue(hasIsland, "在Scene中，" + islandGridPos + "没有Island");

            //print(islandSquares[islandGridPos].transform.name);
            return islandSquares[islandGridPos].GetComponent<IIsland>();
        }  
    }
}
