using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;
using UnityEngine;

namespace Siwei
{
    public class BuildSketch : MonoBehaviour, IBuildSketch
    {
        private bool buildMode = false;
        private bool permitBuild = false;
        private bool buildIslandMode = false;
        private bool buildPropMode = false;
        #region//privates
        private MouseModules _mouseModule;
        private GridMesh _gridMesh;
        private Transform gridMesh_trans;
        private Transform mouseModule_trans;
        #endregion

        #region IObservables
        public IObservable<bool> OnActiveChanged => Observable.Return(buildMode);
        public IObservable<Vector3> OnMouseHoverPositionChanged
        {
            get
            {
                if (_mouseModule is null)
                {
                    _mouseModule = GetComponentInChildren<MouseModules>(true);
                }
                return _mouseModule.OnMouseHoverPositionChanged();
            }
        }
        public IObservable<(string, Vector3)> OnMouseClicked
        {
            get
            {
                if (_mouseModule is null)
                {
                    _mouseModule = GetComponentInChildren<MouseModules>(true);
                }
                string buildObject = GetBuildObject();
                return _mouseModule.OnMouseClicked(buildObject);
            }
            
        }
        #endregion

        #region//Methods
        // 设置是否打开建造模式
        public bool SetBuildMode
        {
            get { return buildMode; }
            set
            {
                if (buildMode != value) {
                    _gridMesh.transform.gameObject.SetActive(value);
                    mouseModule_trans.gameObject.SetActive(value);
                    buildMode = value;
                }                   
            }
        }

        // 是否建造岛屿
        public bool SetBuildIslandMode
        {
            get { return buildIslandMode; }
            set
            {
                if (buildIslandMode)
                {
                    SetBuildPropMode = false;
                    buildIslandMode = true;
                    mouseModule_trans.gameObject.SetActive(true);
                }
                else
                {
                    buildIslandMode = false;
                    mouseModule_trans.gameObject.SetActive(false);
                }
            }
        }

        // 是否建造设施
        public bool SetBuildPropMode
        {
            get { return buildPropMode; }
            set
            {
                if (buildPropMode)
                {
                    SetBuildIslandMode = false;
                    buildPropMode = true;
                    _gridMesh.transform.gameObject.SetActive(true);
                    mouseModule_trans.gameObject.SetActive(true);
                }
                else
                {
                    buildPropMode = false;
                    _gridMesh.transform.gameObject.SetActive(false);
                    mouseModule_trans.gameObject.SetActive(false);
                }
            }
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
        public string GetBuildObject()
        {
            return "";
        }



        #endregion
        private void Awake()
        {
            gridMesh_trans = transform.Find("GridMesh");
            mouseModule_trans = transform.Find("MouseModule");
            _gridMesh = gridMesh_trans.GetComponent<GridMesh>();

            SetBuildMode = false;
        }
    }
}

