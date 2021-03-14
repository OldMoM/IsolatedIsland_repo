using System.Collections;
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
            Debug.Log("sketch:" + sketch.gameObject.name);
            sketch.OnMouseClicked.
                Subscribe(pos => {
                Debug.Log("Clicked Mouse position:[" + pos.x + ","+pos.z+"]"); 
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
            if (Input.GetKeyDown(KeyCode.E)) {
                bool curState = sketch.PermitBuildIsland;
                Debug.Log("Before state change:" + sketch.PermitBuildIsland);
                sketch.PermitBuildIsland = !curState;
                Debug.Log("Permit state changed to:" + sketch.PermitBuildIsland);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                bool isOpen = sketch.SetBuildMode;
                sketch.SetBuildMode = !isOpen;
            }
        }
    }
}

