using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Promotion : CollectableItem
    {
        [SerializeField]
        private float multiplier = 2f;

        [SerializeField]
        private AudioClip promotionClip;

        public static event Action<AudioClip> PlaySound;

        public static event Action<float> PromotionCollected;

        protected override void OnCollect()
        {
            Debug.Log("Collected Promotion!");
            if (PromotionCollected != null)
            {
                PromotionCollected(multiplier);
            }

            if (PromotionCollected != null)
            {
                PlaySound(promotionClip);
            }

            gameObject.SetActive(false);
        }
    }
}
