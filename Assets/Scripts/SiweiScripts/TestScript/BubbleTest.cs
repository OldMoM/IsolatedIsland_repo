using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Siwei
{
    public class BubbleTest : MonoBehaviour
    {
        public GameObject player;
        private ChatBubble chatBubble;
        public bool active;

    private void Start()
        {
            if(chatBubble == null)
            {
                GameObject canvas = GameObject.Find("canvas");
                chatBubble = canvas.GetComponentInChildren<ChatBubble>(true);
            }
            
            
            string[] msg = new string[] { "how", "are", "you"};
            GameObject player = GameObject.Find("Player");
            IObservable<Vector3> pos = Observable.EveryUpdate().Select(_ => player.transform.position);
            //Debug.Log("Player position:"+player.transform.position.x+","+player.transform.position.y+","+player.transform.position.z);
            chatBubble.StartChat(msg, pos);
            
        }

        public void Update()
        {
            active = chatBubble.Active;
        }
    }
}

