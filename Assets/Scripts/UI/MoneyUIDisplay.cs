using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{

    public class MoneyUIDisplay : MonoBehaviour
    {

        private Text text;
        private float currentVal = 0;


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
            if(val < currentVal)
            {
                StopAllCoroutines();
                StartCoroutine(FlashRoutine(Color.red, .10f, 5));
            }
            if (val > currentVal)
            {
                StopAllCoroutines();
                StartCoroutine(FlashRoutine(Color.green, .10f, 5));
            }
            currentVal = val;
            text.text = string.Format("{0}", val.ToString());
        }

        IEnumerator FlashRoutine(Color color, float interval, int repetitions)
        {
            for(int i = 0; i < repetitions; i++)
            {
                text.color = color;
                yield return new WaitForSeconds(interval);
                text.color = Color.black;
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
