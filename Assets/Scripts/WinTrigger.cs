using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class WinTrigger : CollectableItem
    {

        public bool IsFemale;

        public static event Action<bool> LevelWon;

        protected override void OnCollect()
        {
            if (LevelWon != null)
            {
                LevelWon(IsFemale);
            }
            gameObject.SetActive(false);
        }
    }
}
