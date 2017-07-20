using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class LevelTimer : MonoBehaviour
    {
        public static LevelTimer instance;

        public static event Action TimeUp; 

        [SerializeField]
        private float minutesToComplete = 5;

        [SerializeField]
        private bool isPaused = false;

        private float timeLeft;

        private bool timeUp = false;

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
            if (timeLeft <= 0)
            {
                if (TimeUp != null && !timeUp)
                {
                    timeUp = true;
                    TimeUp();

                }
            } else
            {
                timeLeft -= Time.deltaTime;
            }
        }

        public void SubtractTimeLeft(float seconds)
        {
            this.timeLeft -= seconds;
        }
    }
}
