using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class ItemBounce : MonoBehaviour
    {
        [SerializeField]
        private float frequency = 10f;

        [SerializeField]
        private float magnitude = 2f;

        private Vector2 startingPosition;

        private void Start()
        {
            this.startingPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float yDelt = (Mathf.Sin(Time.time * frequency) * magnitude) * Time.deltaTime;
            transform.position = new Vector2(startingPosition.x, startingPosition.y + yDelt);
        }
    }
}
