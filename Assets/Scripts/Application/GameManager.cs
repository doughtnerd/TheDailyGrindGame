using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private Damageable playerDamage;
        private PlayerController playerController;

        #region Lifecycle Functions

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Money.MoneyCollected += OnMoneyCollected;
            Money.PlaySound += OnPlaySound;
            Baby.BabyCollected += OnBabyCollected;
            WinTrigger.LevelWon += OnLevelWon;
            LevelTimer.TimeUp += OnTimeUp;
            Promotion.PromotionCollected += OnPromotionCollected;
            ExplodingCharacter.PlaySound += OnPlaySound;
            Damageable.PlaySound += OnPlaySound;
            JumpingCharacter.PlaySound += OnPlaySound;
            Promotion.PlaySound += OnPlaySound;
            Baby.PlaySound += OnPlaySound;
        }

        private void Update()
        {
            if (playerDamage == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player)
                {
                    playerDamage = player.GetComponent<Damageable>();
                    playerController = player.GetComponent<PlayerController>();
                    playerDamage.Died += OnPlayerDead;
                    playerDamage.Damaged += OnPlayerDamaged;
                }
            }
        }

        //private void OnLevelWasLoaded(int level)
        //{
        //    if (level >= 2)
        //    {
        //        GetComponent<AudioSource>().Stop();
        //    }
        //}
        #endregion

        #region Event Functions

        private void OnPlayerDamaged()
        {
            HeartUIDisplay.Instance.SetHealth(playerDamage.Health);
            Debug.Log("Caught player damaged event");
        }

        private void OnPlayerDead()
        {
            Debug.LogError("Player died, scheduling restart...");
            playerController.ControlsEnabled = false;
            LevelTimer.Instance.Paused = true;
            LoseUIDisplay.Instance.Display(true);
            StartCoroutine(ScheduleRestart(5));
        }

        private void OnMoneyCollected(int value)
        {
            float multiplier = 0;
            StateManager.Instance.TryGetFlag("promotions", out multiplier);
            StateManager.Instance.AddToFlag("money", value * (multiplier + 1));
        }

        private void OnBabyCollected(float value)
        {
            LevelTimer.Instance.SubtractTimeLeft(value);
            TimeUIDisplay.Instance.Flash();
            StateManager.Instance.AddToFlag("money", -150);
        }

        private void OnPromotionCollected(float multiplier)
        {
            StateManager.Instance.AddToFlag("promotions", 1);
        }

        private void OnTimeUp()
        {
            Debug.LogError("Times Up!!!");
            TimeUpText.Instance.Display(true);
            playerController.ControlsEnabled = false;
            StartCoroutine(ScheduleRestart(5));
        }

        private void OnLevelWon(bool isFemale)
        {
            StartCoroutine(WinRoutine(isFemale));
        }

        private IEnumerator WinRoutine(bool isFemale)
        {
            Debug.LogError("Level Won!");
            WinUIDisplay.Instance.Display(true);
            LevelTimer.Instance.Paused = true;
            playerController.ControlsEnabled = false;
            yield return new WaitForSeconds(3f);
            string gender = isFemale ? "female":"male";
            float money = 0;
            StateManager.Instance.TryGetFlag("money", out money);
            float score = + LevelTimer.Instance.TimeLeft + money;
            float highscore = StateManager.Instance.GetFlag(gender + "highscore");
            Debug.Log("score: " + score + " highscore: " + highscore);
            highscore = score > highscore ? score : highscore;
            StateManager.Instance.SaveFlag(gender + "highscore", highscore);

            ScoreUI.Instance.SetScore((int)score);
            ScoreUI.Instance.SetHighscore((int)highscore);
            ScoreUI.Instance.ShowPanel(true);

            StartCoroutine(ScheduleRestart(10));
        }

        private void OnPlaySound(AudioClip clip)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(clip);
        }

        #endregion

        IEnumerator ScheduleRestart(float time)
        {
            yield return new WaitForSeconds(time);
            ScoreUI.Instance.ShowPanel(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StateManager.Instance.SetFlag("money", 0);
            StateManager.Instance.SetFlag("promotions", 0);
            //WinUIDisplay.Instance.Display(false);
        }

        IEnumerator ScheduleAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}
