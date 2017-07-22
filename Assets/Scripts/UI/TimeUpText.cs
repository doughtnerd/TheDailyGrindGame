using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{
    public class TimeUpText : MonoBehaviour
    {
        public static TimeUpText Instance { get; private set; }

        private Text text;

        private void Awake()
        {
            Instance = this;
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
