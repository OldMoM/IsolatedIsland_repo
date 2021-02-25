using Caoye;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public bool isActive;
    IDialogSystem testDialog;

    // Start is called before the first frame update
    void Start()
    {
        testDialog = FindObjectOfType<DialogueSystem>();
        isActive = testDialog.isActive;
        testDialog.StartDialog("3");
    }

    // Update is called once per frame
    void Update()
    {
        isActive = testDialog.isActive;
    }
}
