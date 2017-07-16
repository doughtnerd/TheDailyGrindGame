using UnityEngine;
using System.Collections;
using System;

namespace Grind
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlashColorOnDamaged : MonoBehaviour, IOnDamagedBehavior
    {
        [SerializeField]
        private Color color;


        [SerializeField]
        private int flashCount = 1;

        [SerializeField]
        private float flashInterval;

        public IEnumerator OnDamaged()
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            for (int i = 0; i < flashCount; i++)
            {
                render.color = color;
                yield return new WaitForSeconds(flashInterval / 2);
                render.color = Color.white;
                yield return new WaitForSeconds(flashInterval / 2);
                yield return null;
            }
        }
    }
}