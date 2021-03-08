using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FakeInputsystem : MonoBehaviour
{
    public Subject<Unit> onKeyPressed = new Subject<Unit>();
    // Start is called before the first frame update
    void Start()
    {
        Observable.EveryUpdate()
            .Where(x => Input.GetKeyDown(KeyCode.Q))
            .Subscribe(x =>
            {
                onKeyPressed.OnNext(Unit.Default);
            });
            
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
