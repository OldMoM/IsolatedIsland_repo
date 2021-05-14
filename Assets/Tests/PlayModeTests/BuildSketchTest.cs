using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peixi;
using Siwei;
using UniRx;
public class BuildSketchTest : MonoBehaviour
{
    public Vector2Int[] startIslands;
    IBuildSystem buildSystem => InterfaceArichives.Archive.IBuildSystem;

    private List<Vector3> mouseClickPosition = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        

        //Observable.Timer(System.TimeSpan.FromSeconds(0.2f))
        //    .First()
        //    .Subscribe(x =>
        //    {
        //        foreach (var pos in startIslands)
        //        {
        //            buildSystem.BuildIslandAt(pos);
        //        }
        //    });

        IBuildSketch buildSketch = FindObjectOfType<BuildSketch>();

        buildSketch.OnMouseClicked.Subscribe(x =>
        {
            var gridPos = buildSystem.newWorldToGridPosition(x.Item2);
            print("mouse clicked at" + x.Item2);
            print("add island at " + gridPos);
            buildSystem.BuildIslandAt(gridPos);

            mouseClickPosition.Add(x.Item2);
        });
    }

    private void OnDrawGizmos()
    {
        mouseClickPosition.ForEach(x =>
        {
            Gizmos.DrawWireSphere(x, 0.5f);
        });
    }
}
