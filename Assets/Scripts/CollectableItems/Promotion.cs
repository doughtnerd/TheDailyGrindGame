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

        public static event Action<float> PromotionCollected;

        protected override void OnCollect()
        {
            Debug.Log("Collected Promotion!");
            if (PromotionCollected != null)
            {
                PromotionCollected(multiplier);
            }
            gameObject.SetActive(false);
        }
    }
}
