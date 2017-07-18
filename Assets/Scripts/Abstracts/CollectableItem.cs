using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class CollectableItem : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollect();
        }

        protected abstract void OnCollect();
    }
}
