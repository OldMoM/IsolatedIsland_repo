using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caoye;
using UniRx;
using Peixi;

public class DialogSystemTest : MonoBehaviour
{
    private IDialogSystem dialogSystem;
    // Start is called before the first frame update
    void Start()
    {
        dialogSystem = FindObjectOfType<LvDialogSystem>();

        dialogSystem.OnDialogStart
            .Subscribe(z =>
            {
                //print("start dialog " + z);
            });

        dialogSystem.OnDialogEnd
            .Subscribe(z =>
            {
                //print("end dialog " + z);
            });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
