using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class PlayMusicOnLevelLoad : MonoBehaviour
    {
        [SerializeField]
        private AudioClip musicClip;

        private void Awake()
        {
            SceneManager.sceneLoaded += LevelLoadFunction;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= LevelLoadFunction;
        }

        private void LevelLoadFunction(Scene scene, LoadSceneMode mode)
        {
            MusicManager.Instance.SetClip(this.musicClip);
            MusicManager.Instance.Play();
        }
    }
}
