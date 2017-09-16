using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grind
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject scorePanel;

        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private Text highscoreText;

        public static ScoreUI Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void ShowPanel(bool shouldShow)
        {
            this.scorePanel.SetActive(shouldShow);
        }

        public void SetScore(int score)
        {
            this.scoreText.text = score + "";
            if (score + "".CompareTo(this.highscoreText.text) == 1)
            {
                this.scoreText.color = Color.green;
            }
        }

        public void SetHighscore(int score)
        {
            this.highscoreText.text = score + "";
        }

        //private void OnDisable()
        //{
        //    this.highscoreText.color = Color.black;
        //    this.scoreText.color = Color.black;
        //}
    }
}
