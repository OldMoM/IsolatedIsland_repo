﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Siwei 
{
    public class MouseTest : MonoBehaviour
    {
        private BuildSketch sketch;

        private void Awake()
        {
            sketch = FindObjectOfType<BuildSketch>();
        }
        void Start()
        {
            
            sketch.OnMouseClicked.Subscribe(pos => {
                Debug.Log("Mouse x:" + pos.x);
                Debug.Log("Mouse y:" + pos.y);
                Debug.Log("Mouse z:" + pos.z);
            });
            /*
            sketch.OnMouseHoverPositionChanged.Subscribe(pos =>
            {
                Debug.Log("Mouse x:" + pos.x);
                Debug.Log("Mouse y:" + pos.y);
                Debug.Log("Mouse z:" + pos.z);
            });
            */
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
