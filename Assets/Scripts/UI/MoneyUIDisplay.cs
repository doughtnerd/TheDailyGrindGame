using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{

    public class MoneyUIDisplay : MonoBehaviour
    {

        private Text text;

        // Use this for initialization
        void Start()
        {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            float val = 0;
            StateManager.Instance.TryGetFlag("money", out val);
            text.text = string.Format("Money: ${0}", val.ToString());
        }
    }
}
