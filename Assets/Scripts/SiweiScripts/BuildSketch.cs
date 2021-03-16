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
        public IObservable<Vector3> OnMouseClicked
        {
            get
            {
                if (_mouseModule is null)
                {
                    _mouseModule = GetComponentInChildren<MouseModules>(true);
                }
                return _mouseModule.OnMouseClicked();
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

        // 设置是否允许在该岛块进行建设
        public bool PermitBuildIsland
        {
            get { return permitBuild; }
            set
            {
                permitBuild = value;
            }
        }

        #endregion
        private void Awake()
        {
            gridMesh_trans = transform.Find("GridMesh");
            mouseModule_trans = transform.Find("MouseModule");
            Debug.Log("name:"+gridMesh_trans.gameObject.name);
            _gridMesh = gridMesh_trans.GetComponent<GridMesh>();

            SetBuildMode = false;
        }
    }
}

