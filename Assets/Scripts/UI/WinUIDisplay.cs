using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{
    public class WinUIDisplay : MonoBehaviour
    {

        public static WinUIDisplay Instance { get { return instance; } }

        private static WinUIDisplay instance;

        private Text text;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            text = GetComponent<Text>();
        }

        public void Display(bool display)
        {
            text.enabled = display;
        }
    }
}
