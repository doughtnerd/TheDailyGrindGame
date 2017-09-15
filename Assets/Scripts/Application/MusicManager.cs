using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class MusicManager : MonoBehaviour
    {

        public static MusicManager Instance { get; private set; }

        [SerializeField]
        private AudioSource musicSource;

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

        public void Stop()
        {
            this.musicSource.Stop();
        }

        public void SetClip(AudioClip musicClip)
        {
            this.musicSource.Stop();
            this.musicSource.clip = musicClip;
        }

        public void Play()
        {
            this.musicSource.Play();
        }
    }
}
