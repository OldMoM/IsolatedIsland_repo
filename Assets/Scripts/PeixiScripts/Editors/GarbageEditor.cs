using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UniRx;
using Peixi;

public class GarbageEditor : MonoBehaviour
{
    
    private void OnDrawGizmos()
    {
        var garbageGenerator = GetComponent<GarbageGenerator>();
        List<Transform> boundTransforms = garbageGenerator.generateBoundPoints;
        List<Vector3> spawnPoints = garbageGenerator.randomPoints;
        int listLength = boundTransforms.Count;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < listLength; i++)
        {
            points.Add(boundTransforms[i].position);
        }

        var temp = points[0];
        points.RemoveAt(0);
        points.Add(temp);

        for (int i = 0; i < listLength; i++)
        {
            Handles.DrawLine(boundTransforms[i].position, points[i]);
        }

        spawnPoints
           .ToObservable()
           .Subscribe(x =>
           {
               Gizmos.DrawWireSphere(x, 0.5f);
           });
    }
}
