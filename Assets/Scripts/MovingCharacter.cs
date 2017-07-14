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

        private void Start()
        {
            this.rigid = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            Vector2 velocity = direction.normalized * this.speed * Time.deltaTime;
            this.transform.position = new Vector3(this.transform.position.x + velocity.x, this.transform.position.y + velocity.y);
        }
    }
}
