using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Baby : CollectableItem{


        public static event Action<AudioClip> PlaySound;

        /// <summary>
        /// Event fired when a baby is collected.
        /// </summary>
        public static event Action<float> BabyCollected;

        [SerializeField]
        private float secondsToSubtract = 5f;

        [SerializeField]
        private AudioClip babyClip;

        protected override void OnCollect()
        {
            Debug.Log(string.Format("Collected baby and removed {0} seconds", secondsToSubtract));

            if (PlaySound != null)
            {
                PlaySound(babyClip);
            }

            if (BabyCollected != null)
            {
                BabyCollected(secondsToSubtract);
            }
            gameObject.SetActive(false);
        }
    }
}