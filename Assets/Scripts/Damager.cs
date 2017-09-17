using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(Collider2D))]
    public class Damager : MonoBehaviour
    {
        [SerializeField]
        private int damageAmount = 1;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Damageable d = collision.gameObject.GetComponent<Damageable>();
            if (d!=null)
            {
                Debug.Log(gameObject.name + " is trying to damage: " + collision.gameObject.name);
                d.Damage(damageAmount);
            }
        }
    }
}
