using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Siwei {
    public class MouseModules : MonoBehaviour
    {
        private IObservable<Vector3> mousePos;
        private void Start()
        {
            mousePos = Observable.EveryUpdate().Select(_ => Input.mousePosition);
        }
        public IObservable<Vector3> OnMouseHoverPositionChanged()
        {
            return mousePos.Distinct().Select(_ => GetMouseChangedPos(Input.mousePosition));

        }

        public IObservable<Vector3> OnMouseClicked﻿()
        {
            return mousePos.Where(_ => Input.GetMouseButtonDown(0)).Select(_ => GetGridPosition(Input.mousePosition));

        }

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

        private Vector3 GetMouseChangedPos(Vector3 pos)
        {
            //this.mousePos = pos;
            return GetGridPosition(pos);
        }
    }
}

