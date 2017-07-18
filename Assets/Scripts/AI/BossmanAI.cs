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

        private PatrolBehavior patrol;

        // Use this for initialization
        void Start()
        {
            patrol = GetComponent<PatrolBehavior>();
        }

        // Update is called once per frame
        void Update()
        {
            patrol.Behave();
        }
    }
}
