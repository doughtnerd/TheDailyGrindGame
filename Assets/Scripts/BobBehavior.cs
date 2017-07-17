using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class BobBehavior : MonoBehaviour
    {

        [SerializeField]
        private float detectionDistance = 5f;

        [SerializeField]
        private LayerMask detectableLayer;

        private PatrolBehavior p;
        private ExplodingCharacter e;
        private MovingCharacter m;

        private bool hasDetected = false;

        // Use this for initialization
        void Start()
        {
            p = GetComponent<PatrolBehavior>();
            e = GetComponent<ExplodingCharacter>();
            m = GetComponent<MovingCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!hasDetected)
            {
                p.Behave();

                Debug.DrawRay(transform.position, m.LastDirection * detectionDistance, Color.red);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, m.LastDirection, detectionDistance, detectableLayer);
                if(hit)
                {
                    this.hasDetected = true;
                }
            } else
            {
                m.Move(Vector2.zero);
                e.Explode();
            }
        }
    }
}
