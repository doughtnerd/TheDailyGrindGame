using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public abstract class AOnDamagedBehavior : MonoBehaviour, IOnDamagedBehavior 
    {
        public abstract IEnumerator OnDamaged();
    }
}
