using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class LevelTimer : MonoBehaviour
    {
        public static LevelTimer instance;

        [SerializeField]
        private float minutesToComplete = 5;

        [SerializeField]
        private bool isPaused = false;

        private float timeLeft;

        public float TimeLeft { get { return timeLeft; } set { timeLeft = value; } }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            timeLeft = minutesToComplete * 60;
        }

        void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                StateManager.Instance.SetFlag("levelTime", timeLeft);
            }
        }
    }
}
