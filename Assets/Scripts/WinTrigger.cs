using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class WinTrigger : CollectableItem
    {
        public static event Action LevelWon;

        protected override void OnCollect()
        {
            if (LevelWon != null)
            {
                LevelWon();
            }
            gameObject.SetActive(false);
        }
    }
}
