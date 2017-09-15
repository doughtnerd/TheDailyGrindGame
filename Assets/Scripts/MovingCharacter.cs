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

        private Vector2 lastDirection = Vector2.zero;

        public Vector2 LastDirection
        {
            get
            {
                return lastDirection;
            }
        }

        public float Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }

        private void Start()
        {
            this.rigid = GetComponent<Rigidbody2D>();
            this.anim = GetComponent<Animator>();
        }

        public virtual void Move(Vector2 direction)
        {
            //Normalize direction;
            direction = direction.normalized;

            lastDirection = direction;

            if(direction.x < 0)
            {
                this.facingRight = false;
            } else if (direction.x > 0)
            {
                facingRight = true;
            }

            anim.SetFloat("horizontal", direction.x);
            Vector2 velocity = direction * this.speed * Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x + velocity.x, this.transform.position.y + velocity.y);
        }

        public void MoveHorizontal(int direction)
        {
            Vector2 dir = new Vector2(direction, 0);
            Move(dir);
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
