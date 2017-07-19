using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    [RequireComponent(typeof(MovingCharacter), typeof(JumpingCharacter))]
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        private bool controlsEnabled = true;

        private MovingCharacter move;
        private JumpingCharacter jump;

        public bool ControlsEnabled { get { return controlsEnabled; } set { controlsEnabled = value; } }

        private void Start()
        {
            this.move = GetComponent<MovingCharacter>();
            this.jump = GetComponent<JumpingCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (ControlsEnabled)
            {
                Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);

                this.move.Move(moveDirection);

                if (Input.GetButtonDown("Jump"))
                {
                    this.jump.Jump();
                }
            }
        }
    }
}
