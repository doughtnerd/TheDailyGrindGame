using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class JumpingCharacter : MonoBehaviour
    {
        [SerializeField]
        private float jumpPower = 5f;

        [SerializeField]
        private int jumpCount = 2;

        //[SerializeField]
        //private string groundLayerName;

        [SerializeField]
        private LayerMask groundLayer;

        private bool isGrounded;
        private int currentJump = 0;


        private Rigidbody2D rigid;
        private Animator anim;


        private void Start()
        {
            this.rigid = GetComponent<Rigidbody2D>();
            this.anim = GetComponent<Animator>();
        }

        public void Jump()
        {
            if(isGrounded || currentJump < jumpCount)
            {
                this.isGrounded = false;

                if(this.currentJump == 0)
                {
                    this.anim.SetBool("jumping", true);
                }
                this.currentJump++;

                if(this.currentJump > 0)
                {
                    this.rigid.velocity = Vector2.zero;
                } 
                this.rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }

        private void Update()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log(LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == groundLayer.value);
            //if(collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
            if(LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer)) == groundLayer.value)
            {
                this.isGrounded = true;
                this.anim.SetBool("jumping", false);
                this.currentJump = 0;
            }
        }
    }
}
