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
            StartCoroutine(TextAnimation(this.scoreText, score, score / 50));
        }

        public void SetHighscore(int score)
        {
            StartCoroutine(TextAnimation(this.highscoreText, score, score/20));
        }

        private IEnumerator TextAnimation(Text text, int score, int increment)
        {
            int start = 0;
            while(start < score)
            {
                start = start+increment > score ? score:start+increment;
                yield return new WaitForEndOfFrame();
                text.text = start + "";
            }
        }

        //private void OnDisable()
        //{
        //    this.highscoreText.color = Color.black;
        //    this.scoreText.color = Color.black;
        //}
    }
}
