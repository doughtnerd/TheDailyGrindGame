using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class LevelTimer : MonoBehaviour
    {
        public static LevelTimer Instance { get; private set; }

        public static event Action TimeUp; 

        [SerializeField]
        private float minutesToComplete = 5;

        [SerializeField]
        private bool paused = false;

        private float timeLeft;

        private bool timeUp = false;

        public float TimeLeft { get { return timeLeft; } set { timeLeft = value; } }

        public bool Paused { get { return paused; } set { paused = value; } }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            timeLeft = minutesToComplete * 60;
        }

        void Update()
        {
            if (!paused)
            {
                if (timeLeft <= 0)
                {
                    if (TimeUp != null && !timeUp)
                    {
                        timeUp = true;
                        TimeUp();
                    }
                }
                else
                {
                    timeLeft -= Time.deltaTime;
                }
            }
        }

        public void SubtractTimeLeft(float seconds)
        {
            this.timeLeft -= seconds;
        }
    }
}
