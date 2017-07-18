using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Money : CollectableItem
    {
        [SerializeField]
        private int worth;

        protected override void OnCollect()
        {
            Debug.Log(string.Format("Picked up {0} money", worth));
            StateManager.Instance.AddToFlag("money", worth);
            gameObject.SetActive(false);
        }
    }
}
