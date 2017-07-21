using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Money : CollectableItem
    {
        public static event Action<int> MoneyCollected;
        public static event Action<AudioClip> PlaySound;

        [SerializeField]
        private int worth;

        [SerializeField]
        private AudioClip collectSound;

        protected override void OnCollect()
        {
            Debug.Log(string.Format("Picked up {0} money", worth));
            if (MoneyCollected != null)
            {
                MoneyCollected(worth);
            }
            if (collectSound != null)
            {
                PlaySound(collectSound);
            }
            gameObject.SetActive(false);
        }

        //IEnumerator CollectRoutine()
        //{
        //    Debug.Log(string.Format("Picked up {0} money", worth));
        //    if (MoneyCollected != null)
        //    {
        //        MoneyCollected(worth);
        //    }
        //    collectSound.Play();
        //    yield return new WaitForSeconds(collectSound.clip.length);
        //    gameObject.SetActive(false);
        //}

    }
}
