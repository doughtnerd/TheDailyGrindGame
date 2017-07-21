using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class HeartUIDisplay : MonoBehaviour
    {
        public static HeartUIDisplay Instance { get { return instance; } }

        private static HeartUIDisplay instance;

        private Animator anim;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void SetHealth(int health)
        {
            Debug.Log("Setting player health to..." + health);
            this.anim.SetInteger("playerHealth", health);
        }
    }
}
