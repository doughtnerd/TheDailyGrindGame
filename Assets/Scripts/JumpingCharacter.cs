using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpingCharacter : MonoBehaviour
    {
        [SerializeField]
        private float jumpPower = 5f;

        private Rigidbody2D rigid;

        private void Start()
        {
            this.rigid = GetComponent<Rigidbody2D>();
        }

        public void Jump()
        {
            this.rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
