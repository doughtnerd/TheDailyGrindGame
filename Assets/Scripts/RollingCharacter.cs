using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RollingCharacter : MonoBehaviour
    {
        [SerializeField]
        private float torque = 1f;

        private Rigidbody2D rigid;

        public float Torque
        {
            get
            {
                return this.torque;
            }
        }

        private void Start()
        {
            this.rigid = GetComponent<Rigidbody2D>();
        }

        public virtual void Roll(Vector2 direction)
        {
            this.rigid.AddTorque(-direction.x * torque, ForceMode2D.Force);
            //anim.SetFloat("horizontal", direction.x);
            //Vector2 velocity = direction.normalized * this.speed * Time.deltaTime;
            //this.transform.position = new Vector3(this.transform.position.x + velocity.x, this.transform.position.y + velocity.y);
        }
    }
}
