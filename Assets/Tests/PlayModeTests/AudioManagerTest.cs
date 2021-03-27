using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siwei;
using Peixi;
using UniRx;

public class AudioManagerTest : MonoBehaviour
{
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AudioManager>();

        AudioEvents.StartAudio("OnRainDay");

        AudioEvents.OnAudioStart
            .Where(x => x == "OnRainDay")
            .Subscribe(x =>
            {
                print(x);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
