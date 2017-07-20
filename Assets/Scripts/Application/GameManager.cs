using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance { get { return instance; } }

        private Damageable playerDamage;
        private PlayerController controller;

        #region Lifecycle Functions

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Money.MoneyCollected += OnMoneyCollected;
            Baby.BabyCollected += OnBabyCollected;
            WinTrigger.LevelWon += OnLevelWon;
            LevelTimer.TimeUp += OnTimeUp;
        }

        private void Update()
        {
            if (playerDamage == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player)
                {
                    playerDamage = player.GetComponent<Damageable>();
                    controller = player.GetComponent<PlayerController>();
                    playerDamage.Died += OnPlayerDead;
                }
            }
        }

        //private void OnDestroy()
        //{
        //    playerDamage.Died -= OnPlayerDead;
        //}

        //private void OnDisable()
        //{
        //    playerDamage.Died -= OnPlayerDead;
        //}

        #endregion

        #region Event Functions

        private void OnPlayerDead()
        {
            Debug.LogError("Player died, scheduling restart...");
            controller.ControlsEnabled = false;
            StartCoroutine(ScheduleRestart());
        }

        private void OnMoneyCollected(int value)
        {
            StateManager.Instance.AddToFlag("money", value);
        }

        private void OnBabyCollected(float value)
        {
            LevelTimer.instance.SubtractTimeLeft(value);
        }

        private void OnTimeUp()
        {
            Debug.LogError("Times Up!!!");
            controller.ControlsEnabled = false;
            StartCoroutine(ScheduleRestart());
        }

        private void OnLevelWon()
        {
            Debug.LogError("Level Won!");
            StartCoroutine(ScheduleRestart());
        }

        #endregion

        IEnumerator ScheduleRestart()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StateManager.Instance.SetFlag("money", 0);
        }
    }
}
