using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Siwei;
using System;

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
                .Subscribe(y =>
                {
                    print("The mouse click position: " + y);
                });

        activeBuildSketch
            .Subscribe(y =>
            {
                buildSketch.SetBuildMode = y;
            });


        //Observable.Timer(TimeSpan.FromSeconds(1))
        //    .Subscribe(x =>
        //    {
        //        buildSketch.OnMouseClicked
        //        .Subscribe(y =>
        //        {
        //            print("The mouse click position: " + y);
        //        });

        //        activeBuildSketch
        //            .Subscribe(y =>
        //            {
        //                buildSketch.SetBuildMode = y;
        //            });
        //    });
    }

    
}
