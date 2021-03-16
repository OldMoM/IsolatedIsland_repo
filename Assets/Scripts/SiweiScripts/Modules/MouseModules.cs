using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Siwei {
    public class MouseModules : MonoBehaviour
    {
        private IObservable<Vector3> mousePos;
        public bool isActive;

        private void Start()
        {
            mousePos = Observable.EveryUpdate().Select(_ => Input.mousePosition);
        }
        public IObservable<Vector3> OnMouseHoverPositionChanged()
        {
            if (mousePos == null)
            {
                mousePos = Observable.EveryUpdate().Select(_ => Input.mousePosition);
            }
            return mousePos.Distinct()
                           .Where(_ =>gameObject.activeSelf)
                           .Select(_ => GetGridPosition(Input.mousePosition));

        }

        public IObservable<Vector3> OnMouseClicked﻿()
        {
            if (mousePos == null)
            {
                mousePos = Observable.EveryUpdate().Select(_ => Input.mousePosition);
            }
            return mousePos.Where(_ => Input.GetMouseButtonDown(0) && gameObject.activeSelf)
                           .Select(_ => GetGridPosition(Input.mousePosition));
        }

        /// <summary>
        /// 得到鼠标在y=0平面的投影三维坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Vector3 GetGridPosition(Vector3 pos)
        {
            Plane plane = GridMesh.plane;
            float distance;
            Vector3 worldPos;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (plane.Raycast(ray, out distance))
            {
                worldPos = ray.GetPoint(distance);
                return new Vector3(worldPos.x, worldPos.y, worldPos.z);
            }
            Debug.Log("Ray did not get to the target plane");
            return new Vector3(10000, 10000, 10000);
        }
    }
}

