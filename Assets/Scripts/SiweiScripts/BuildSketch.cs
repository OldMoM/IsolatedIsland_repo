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

        #region//privates
        private MouseModules _mouseModule;
        private GridMesh _gridMesh;
        #endregion

        #region IObservables
        public IObservable<bool> OnActiveChanged => Observable.Return(buildMode);
        public IObservable<Vector3> OnMouseHoverPositionChanged => _mouseModule.OnMouseHoverPositionChanged();
        public IObservable<Vector3> OnMouseClicked => _mouseModule.OnMouseClicked();

        #endregion

        #region//Methods
        public bool PermitBuildIsland
        {
            get{ return buildMode; }
            set{
                if (buildMode != value) {
                    _gridMesh.gameObject.SetActive(value);
                    buildMode = value;
                }                   
                
            }
        }

        #endregion
        private void Awake()
        {
            _mouseModule = GetComponentInChildren<MouseModules>();
            _gridMesh = FindObjectOfType<GridMesh>();
        }

        // 测试是否进入建造模式
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A)) {
                Debug.Log("A clicked");
                PermitBuildIsland = true;
            }
            if (Input.GetKeyUp(KeyCode.B))
            {
                Debug.Log("B clicked");
                PermitBuildIsland = false;
            }
        }
    }
}

