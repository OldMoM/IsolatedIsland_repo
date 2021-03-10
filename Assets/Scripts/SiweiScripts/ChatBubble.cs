using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Threading;

namespace Siwei
{
    public class ChatBubble : MonoBehaviour, IChatBubble
    {
        private bool active = false;
        private Text text;
        public float duration;

        private void Awake()
        {
            duration = 2;
            text = GetComponentInChildren<Text>();
        }

        // Test
        private void Start()
        {
            string[] msg = new string[]{ "how", "are", "you" };
            GameObject player = GameObject.Find("Player");
            IObservable<Vector3> pos = Observable.EveryUpdate().Select(_ => player.transform.position);
            StartChat(msg, pos);
        }


        public void StartChat(string[] msg, IObservable<Vector3> onPlayerPositionChanged) {

            StartCoroutine(Chat(msg, onPlayerPositionChanged));            
        }
        public bool Active { get { return active; } }

        private IEnumerator Chat(string[] msg, IObservable<Vector3> onPlayerPositionChanged)
        {
            Debug.Log("Message length is:" + msg.Length);
            int i = 0;
            DateTime curTime = DateTime.Now;
            onPlayerPositionChanged.TakeWhile(_ => i < msg.Length)
                .Subscribe(pos =>
                {
                    DrawBubble(msg[i], pos);
                    
                });
            
            while (i < msg.Length)
            {
                
                yield return new WaitForSeconds(duration);
                i++;
                Debug.Log(i);
            }
            

        }

        private void DrawBubble(string msg, Vector3 pos)
        {
            this.transform.position = Camera.main.transform.InverseTransformPoint(pos);
            text.text = msg;
        }

    }
}

