using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Peixi
{
    /// <summary>
    /// 用来展示一些提示信息
    /// </summary>
    public class ShowMessage : MonoBehaviour
    {
        public static ShowMessage singlton;
        private Text msgText;
        // Start is called before the first frame update
        void Awake()
        {
            singlton = this;
            msgText = GetComponent<Text>();
        }
        public void Message(string msg)
        {
            msgText.text = msg;
        }

    }
}
