using UnityEngine;
using System.Collections;
using System;

namespace Grind
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlashColorOnDamage : AOnDamagedBehavior
    {
        [SerializeField]
        private Color color;

        [SerializeField]
        private int flashCount = 1;

        [SerializeField]
        private float flashInterval;

        public override IEnumerator OnDamaged()
        {
            SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
            for (int i = 0; i < flashCount; i++)
            {
                render.color = color;
                yield return new WaitForSeconds(flashInterval);
                render.color = Color.white;
                yield return null;
            }
        }
    }
}
