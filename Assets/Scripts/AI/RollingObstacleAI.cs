using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class RollingObstacleAI : MonoBehaviour
    {
        [SerializeField]
        private float detectionRadius = 15f;

        [SerializeField]
        private LayerMask detectableLayer;

        private bool hasDetected = false;

        private RollingCharacter roll;

        // Use this for initialization
        void Start()
        {
            roll = GetComponent<RollingCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (hasDetected)
            {
                roll.Roll(Vector2.left);
            } else
            {
                Collider2D coll = Physics2D.OverlapCircle(transform.position, detectionRadius, detectableLayer);
                if(coll)
                {
                    this.hasDetected = true;
                }
            }

        }
    }
}
