using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class ItemBounce : MonoBehaviour
    {
        [SerializeField]
        private float omegaY = 10f;

        [SerializeField]
        private float amplitudeY = 2f;

        private Vector2 startingPosition;

        private void Start()
        {
            this.startingPosition = transform.position;
        }

        float index;
        public void Update()
        {
            index += Time.deltaTime;
            float y = amplitudeY * Mathf.Sin(omegaY * index);
            transform.position = new Vector3(transform.position.x, y + startingPosition.y, 0);
        }
    }
}
