using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{
    public class LoseUIDisplay : MonoBehaviour
    {
        public static LoseUIDisplay Instance { get { return instance; } }

        private static LoseUIDisplay instance;

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
