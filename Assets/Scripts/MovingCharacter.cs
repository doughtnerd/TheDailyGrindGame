using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovingCharacter : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;

        private Rigidbody2D rigid;

        private Animator anim;

        private bool facingRight = true;

        public float Speed
        {
            get
            {
                return this.speed;
            }
        }

        private void Start()
        {
            this.rigid = GetComponent<Rigidbody2D>();
            this.anim = GetComponent<Animator>();
        }

        public void Move(Vector2 direction)
        {
            if(direction.x < 0)
            {
                this.facingRight = false;
            } else
            {
                facingRight = true;
            }

            anim.SetFloat("horizontal", direction.x);
            Vector2 velocity = direction.normalized * this.speed * Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x + velocity.x, this.transform.position.y + velocity.y);
        }

        private void FixedUpdate()
        {

            if (!facingRight)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
