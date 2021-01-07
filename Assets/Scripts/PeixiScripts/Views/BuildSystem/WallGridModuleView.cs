using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace Peixi
{
    public class WallGridModuleView : MonoBehaviour
    {
        WallGridModulePresenter presenter;
        private void Awake()
        {
            presenter = GetComponent<WallGridModulePresenter>();
        }
        // Start is called before the first frame update
        void Start()
        {
            presenter.OnWallAdded
                .Subscribe(x =>
                {
                    CreatWallCubeAt(x.Key);
                });
        }
        protected void CreatWallCubeAt(Vector2Int gridPos)
        {
       
        }
    }
}
