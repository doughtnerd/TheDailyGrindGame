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

        private void OnDestroy()
        {
            playerDamage.Died -= OnPlayerDead;
        }

        private void OnDisable()
        {
            playerDamage.Died -= OnPlayerDead;
        }

        private void OnPlayerDead()
        {
            Debug.LogError("Player died, scheduling restart...");
            controller.ControlsEnabled = false;
            StartCoroutine(ScheduleRestart());
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
            } else
            {
                //if (playerDamage.IsDead)
                //{
                //    Debug.LogError("Player is dead");
                //    controller.ControlsEnabled = false;
                //    StartCoroutine(ScheduleRestart());
                //}

                //if (LevelTimer.instance.TimeLeft <= 0)
                //{
                //    Debug.LogError("Time is up");
                //    controller.ControlsEnabled = false;
                //    StartCoroutine(ScheduleRestart());
                //}
            }
        }

        IEnumerator ScheduleRestart()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
