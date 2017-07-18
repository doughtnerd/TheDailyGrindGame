using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Baby : CollectableItem{

        [SerializeField]
        private float timeToSubtract = 5f;

        protected override void OnCollect()
        {
            Debug.Log(string.Format("Collected baby and removed {0} seconds", timeToSubtract));
            gameObject.SetActive(false);
        }
    }
}