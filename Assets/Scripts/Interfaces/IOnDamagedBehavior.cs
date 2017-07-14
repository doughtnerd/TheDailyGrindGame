using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public interface IOnDamagedBehavior
    {
        IEnumerator OnDamaged();
    }
}
