using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class ResetGameData : MonoBehaviour
    {

        public void ResetGame()
        {
            StateManager.Instance.SetFlag("money", 0);
            StateManager.Instance.SetFlag("promotions", 0);
        }
    }
}
