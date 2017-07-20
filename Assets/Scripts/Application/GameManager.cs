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
            Money.PlaySoundEvent += OnPlaySound;
            Baby.BabyCollected += OnBabyCollected;
            WinTrigger.LevelWon += OnLevelWon;
            LevelTimer.TimeUp += OnTimeUp;
            Promotion.PromotionCollected += OnPromotionCollected;
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
            float multiplier = 0;
            StateManager.Instance.TryGetFlag("promotions", out multiplier);
            StateManager.Instance.AddToFlag("money", value * (multiplier + 1));
        }

        private void OnBabyCollected(float value)
        {
            LevelTimer.instance.SubtractTimeLeft(value);
        }

        private void OnPromotionCollected(float multiplier)
        {
            StateManager.Instance.AddToFlag("promotions", 1);
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

        private void OnPlaySound(AudioClip clip)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
        }

        #endregion

        IEnumerator ScheduleRestart()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StateManager.Instance.SetFlag("money", 0);
            StateManager.Instance.SetFlag("promotions", 0);
        }
    }
}
