using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Grind
{
    [RequireComponent(typeof(MovingCharacter))]
    public class PatrolBehavior : MonoBehaviour
    {

        [SerializeField]
        private bool behaviorEnabled = true;

        [SerializeField]
        private Vector2 defaultDirection = Vector2.left;

        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        private float speedMultiplier = 3f;

        private bool swap = false;

        private MovingCharacter move;

        void Start()
        {
            this.move = GetComponent<MovingCharacter>();
        }

        void Update()
        {
            if (behaviorEnabled)
            {
                //Convert position to vector 2.
                Vector2 myPosition = transform.position;

                //Get the direction that character is going to move 
                Vector2 direction = swap ? -defaultDirection : defaultDirection;

                //Scale the direction with a padded speed to get a buffered direction.
                direction.Scale(new Vector2(move.Speed * 3, 0));

                Vector2 nextPosition = myPosition + direction * Time.deltaTime;

                Debug.DrawRay(nextPosition, Vector2.down * speedMultiplier);

                RaycastHit2D hit = Physics2D.Raycast(nextPosition, Vector2.down, speedMultiplier, groundLayer);
                if (hit)
                {
                    move.Move(direction);
                }
                else
                {
                    swap = !swap;
                }
            }
        }
    }

}