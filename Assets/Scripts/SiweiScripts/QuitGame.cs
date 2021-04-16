using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [HideInInspector]
    public GameObject quitGameObj;
    private void Awake()
    {
        quitGameObj = this.transform.Find("QuitPanel").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitGameObj.SetActive(!quitGameObj.activeSelf);
        }
    }

    public void Quit()
    {
        Debug.Log("Click Quit button");
        Application.Quit();
    }

    public void Cancel()
    {
        Debug.Log("Click Cancel button");
        quitGameObj.SetActive(false);
    }
}
