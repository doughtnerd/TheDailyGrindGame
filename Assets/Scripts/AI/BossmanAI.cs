using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class BossmanAI : MonoBehaviour
    {
        [SerializeField]
        LayerMask detectableLayer;

        [SerializeField]
        private float detectableDistance;

        private bool hasDetected;

        private PatrolBehavior patrol;
        private MovingCharacter move;


        // Use this for initialization
        void Start()
        {
            patrol = GetComponent<PatrolBehavior>();
            move = GetComponent<MovingCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, move.LastDirection, detectableDistance, detectableLayer);
            if (hit)
            {
                move.Speed = 7;
            }  else
            {
                move.Speed = 3;
            }
            patrol.Behave();
        }
    }
}
