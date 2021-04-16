using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siwei;
using Peixi;

public class AudioPlayTestScript : MonoBehaviour
{
    AudioController audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioController>();
        audioManager.PlayAudio(AudioRegistration.audioTable["OnMainControlBtnPressed"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
