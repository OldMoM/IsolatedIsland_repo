using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

[Serializable]
public class IslandModel 
{
    public IntReactiveProperty durability;
    public bool isActive;
    public Subject<Vector2Int> onIslandDestoryed;

    public IObservable<int> onDayStart;
    public IObservable<int> onDayEnd;

    public Vector2Int positionInGrid;

    public GameObject attachedObject;
}
