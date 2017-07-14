using UnityEngine;
using System.Collections;

namespace Grind
{
    /// <summary>
    /// Attach to a camera object to enable target following.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// The target GameObject to follow.
        /// </summary>
        [SerializeField]
        private GameObject target;

        /// <summary>
        /// The camera z-axis offset.
        /// </summary>
        //[SerializeField]
        private float offset;

        /// <summary>
        /// Whether or not to use smooth following.
        /// </summary>
        [SerializeField]
        private bool smoothFollow = false;

        /// <summary>
        /// The follow velocity when using smooth follow.
        /// </summary>
        private Vector3 followVelocity = Vector3.zero;

        /// <summary>
        /// The follow delay when using smooth follow.
        /// </summary>
        public float followTime = .5f;

        /// <summary>
        /// Sets this objects target.
        /// </summary>
        /// <param name="target">The target game object.</param>
        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public GameObject GetTarget()
        {
            return this.target;
        }

        void LateUpdate()
        {
            if (target != null)
            {
                Vector3 destination = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
                transform.position = smoothFollow ? Vector3.SmoothDamp(transform.position, destination, ref followVelocity, followTime) : destination;

            }
        }
    }
}