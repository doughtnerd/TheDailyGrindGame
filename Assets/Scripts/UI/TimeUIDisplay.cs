using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{
    public class TimeUIDisplay : MonoBehaviour
    {

        public static TimeUIDisplay Instance { get; private set; }

        private Text text;

        private void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = Mathf.RoundToInt(LevelTimer.instance.TimeLeft).ToString();
        }

        public void Flash()
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine(Color.red, .1f, 5));
        }

        IEnumerator FlashRoutine(Color color, float interval, int repetitions)
        {
            Debug.Log("Running flash routine");
            for (int i = 0; i < repetitions; i++)
            {
                text.color = color;
                yield return new WaitForSeconds(interval);
                text.color = Color.black;
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
