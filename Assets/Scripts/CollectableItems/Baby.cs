using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Baby : CollectableItem{

        /// <summary>
        /// Event fired when a baby is collected.
        /// </summary>
        public static event Action<float> BabyCollected;

        [SerializeField]
        private float secondsToSubtract = 5f;

        protected override void OnCollect()
        {
            Debug.Log(string.Format("Collected baby and removed {0} seconds", secondsToSubtract));
            if (BabyCollected != null)
            {
                BabyCollected(secondsToSubtract);
            }
            gameObject.SetActive(false);
        }
    }
}