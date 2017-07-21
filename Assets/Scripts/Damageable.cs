using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grind
{
    public class Damageable : MonoBehaviour
    {
        public static event Action<AudioClip> PlaySound;

        public event Action Died;

        public event Action Damaged;

        [SerializeField]
        private int maxHealth = 3;

        [SerializeField]
        private int health = 3;

        [SerializeField]
        private float iFrames = 1.5f;

        [SerializeField]
        private AudioClip damageClip;

        private float nextDamageTime;

        private bool isDead = false;

        private Animator anim;

        public int Health { get { return health; } }

        public bool IsDead { get { return isDead; } }

        private void Start()
        {
            this.anim = GetComponent<Animator>();
        }

        public void Damage(int amount)
        {
            if(health>0)
            {
                if(Time.time > nextDamageTime)
                {
                    health -= amount;
                    health = health < 0 ? 0 : health;

                    this.anim.SetTrigger("damage");

                    if (PlaySound != null)
                    {
                        PlaySound(damageClip);
                    }

                    if (Damaged != null)
                    {
                        Damaged();
                    }

                    nextDamageTime = Time.time + iFrames;

                    IOnDamagedBehavior[] b = GetComponents<IOnDamagedBehavior>();
                    for (int i = 0; i < b.Length; i++)
                    {
                        StartCoroutine(b[i].OnDamaged());
                    }

                    Debug.Log(gameObject.name + "'s health is: " + health);
                }
            }

            if (health == 0 && !isDead)
            {
                this.anim.SetTrigger("die");
                isDead = true;
                if (Died != null)
                {
                    Died();
                }
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
