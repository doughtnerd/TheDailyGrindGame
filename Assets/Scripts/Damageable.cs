using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 3;

        [SerializeField]
        private int health = 3;

        [SerializeField]
        private float iFrames = 1.5f;

        [SerializeField]
        private AOnDamagedBehavior[] onDamagedBehaviors;

        private float nextDamageTime;

        public void Damage(int amount)
        {
            if(health>0)
            {
                if(Time.time > nextDamageTime)
                {
                    health -= amount;
                    health = health < 0 ? 0 : health;

                    nextDamageTime = Time.time + iFrames;

                    if (onDamagedBehaviors != null)
                    {
                        for (int i = 0; i < onDamagedBehaviors.Length; i++)
                        {
                            StartCoroutine(onDamagedBehaviors[i].OnDamaged());
                        }
                    }

                    Debug.Log(gameObject.name + "'s health is: " + health);
                }
            }

            if (health == 0)
            {
                Debug.Log(gameObject.name + " is dead");
            }
        }

        public void Heal(int amount)
        {
            health += amount;
            health = health > maxHealth ? maxHealth : health;
        }
    }
}
