using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Siwei;

public class BuildSketchTest : MonoBehaviour
{
    public BoolReactiveProperty activeBuildSketch = new BoolReactiveProperty();
    public BoolReactiveProperty permitBuildIsland = new BoolReactiveProperty();

    IBuildSketch buildSketch;
    // Start is called before the first frame update
    void Start()
    {
        buildSketch = FindObjectOfType<BuildSketch>();
        permitBuildIsland.Subscribe(x =>
        {
            buildSketch.PermitBuildIsland = x;
        });

        buildSketch.OnMouseClicked
            .Subscribe(x =>
            {
                print("The mouse click position: " + x);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
