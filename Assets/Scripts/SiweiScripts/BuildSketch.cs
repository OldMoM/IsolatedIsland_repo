using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;
using UnityEngine;
using System.Linq;
using Peixi;
using UnityEngine.Assertions;

namespace Siwei
{
    public class BuildSketch : MonoBehaviour, IBuildSketch
    {
        private bool buildMode = false;
        private bool permitBuild = false;
        public bool buildIslandMode = false;
        private bool buildPropMode = false;

        private string buildObject; // facility under construction

        #region privates
        public MouseModules _mouseModule;
        public GridMesh _gridMesh;
        private GameObject gridMesh_obj;
        private GameObject mouseModule_obj;

        public BuildSystem _buildSystem;

        #endregion

        #region public

        public BuildSystem BuildSystem
        {
            get
            {
                if (_buildSystem is null)
                {
                    _buildSystem = FindObjectOfType<BuildSystem>();
                }
                return _buildSystem;
            }
        }
        public List<Vector2Int> IslandBuildSeq;

        //[HideInInspector] public List<Vector2Int> islandsPosList;
        public List<Vector2Int> islandsPosList;

        public int buildSeq = 0;

        #endregion

        #region IObservables

        /// <summary>
        /// item1: string--> buildItem(island/prop)
        /// item2: Vector3--> buildPosition
        /// </summary>
        public Subject<(string, Vector3)> buildRequest = new Subject<(string, Vector3)>();

        public IObservable<bool> onBuildPropModeChanged => Observable.Return(buildPropMode);
        public IObservable<bool> OnActiveChanged => Observable.Return(buildMode);
        public IObservable<Vector3> OnMouseClickedPos 
        {
            get 
            {
                if (_mouseModule is null)
                {
                    _mouseModule = GetComponentInChildren<MouseModules>(true);
                }
                return _mouseModule.OnMouseClickedPos(); 
            }
        }

    public IObservable<Vector3> OnMouseHoverPosition
        {
            get
            {
                if (_mouseModule is null)
                {
                    _mouseModule = GetComponentInChildren<MouseModules>(true);
                }

                return _mouseModule.OnMouseHoverPosition();
            }
        }
        public IObservable<(string, Vector3)> OnMouseClicked
        {
            get
            {
                
                return buildRequest;
            }

        }


        #endregion

        private void Start()
        {
            buildRequest.Subscribe(x =>
            {
                if (x.Item1 == "island")
                {
                    ToggleIslandBuild();
                    ++buildSeq;             
                }
                else
                {
                    TogglePropBuild();
                }

                BuildObject = string.Empty; // Clear the current build object
            });

        }

        #region//Methods
        // 设置是否打开建造模式
        public bool SetBuildMode
        {
            get { return buildMode; }
            set
            {
                if (buildMode != value)
                {
                    //_gridMesh.transform.gameObject.SetActive(value);
                    //_mouseModule.transform.gameObject.SetActive(value);


                    buildMode = value;
                    
                    if(_buildSystem is null)
                    {
                       
                    }
                }
            }
        }

        public void BuildIsland(bool state)
        {
            if (state)
            {
                buildObject = "island";
            }
            else
            {
                buildObject = string.Empty;
            }
        }

        public void BuildProp(bool state)
        {
            
            if (state)
            {
                //_gridMesh.DrawOnPropMode(islandsPosList);
            }
        }

        public void ToggleIslandBuild()
        {
            //Debug.Log("Before pressing, buildIslandMode is: " + buildIslandMode);
            buildIslandMode = !buildIslandMode;
            SetModuleActive(buildIslandMode);
            BuildIsland(buildIslandMode);
        }

        public void TogglePropBuild()
        {
            buildPropMode = !buildPropMode;
            SetModuleActive(buildPropMode);
            islandsPosList = GetAllIslands();
            BuildProp(buildPropMode);
        }

        public bool GetIslandMode()
        {
            return buildIslandMode;
        }

        public bool GetPropMode()
        {
            return buildPropMode;
        }

        // 设置是否允许在该岛块进行建设
        public bool PermitBuildIsland
        {
            get { return permitBuild; }
            set
            {
                permitBuild = value;
            }
        }


        // 得到建造的对象（岛屿/设施名）
        public string BuildObject
        {
            get{ return buildObject; }
            set{
                buildObject = value;
                Debug.Log("Select Prop type:" + value);
            }
        }
        
        public List<Vector2Int> GetAllIslands()
        {
            Debug.Log("Island count is:" + BuildSystem._islandGridModule.model.model.gridData.Keys.Count);
            return BuildSystem._islandGridModule.model.model.gridData.Keys.ToList();
        }

        public bool CheckIslandExist(Vector2Int pos)
        {
            return _buildSystem._islandGridModule.CheckThePositionHasIsland(pos);
        }

        public bool CheckFacilityExist(Vector2Int pos)
        {
            return _buildSystem._facilityModule.HasFacility(pos);
        }



        #endregion

        private void SetModuleActive(bool isActive)
        {
            Assert.IsNotNull( _gridMesh);
            Assert.IsNotNull(_gridMesh.gameObject);
            _gridMesh.gameObject.SetActive(isActive);
            _mouseModule.gameObject.SetActive(isActive);
        }

        public bool CheckBuildRequirement(string facility)
        {

            return true;
        } 

        private void Awake()
        {
            Debug.Log("Island build Seq count is:" + IslandBuildSeq.Count);
            //Debug.Log("_gridMesh:" + _gridMesh==null);
            //Debug.Log("_mouseModule:" + _mouseModule == null);
            _gridMesh = transform.GetComponentInChildren<GridMesh>(true);
            _mouseModule = transform.GetComponentInChildren<MouseModules>(true);
            //ToggleIslandBuild();
            Debug.Log("_gridMesh:" + _gridMesh == null);
            Debug.Log("_mouseModule:" + _mouseModule == null);
            Assert.IsNotNull(_gridMesh);
            //_gridMesh.gameObject.SetActive(true);

            _buildSystem = FindObjectOfType<BuildSystem>();

            Assert.IsNotNull(_buildSystem, "_buildSystem is empty");

        }
    }
}

