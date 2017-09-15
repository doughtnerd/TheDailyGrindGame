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

        [SerializeField]
        private SimpleTouchController moveController;

        [SerializeField]
        private SimpleTouchController jumpController;

        private MovingCharacter move;
        private JumpingCharacter jump;
        private Vector2 moveDirection;

        public bool ControlsEnabled { get { return controlsEnabled; } set { controlsEnabled = value; } }

        private void Start()
        {
            this.move = GetComponent<MovingCharacter>();
            this.jump = GetComponent<JumpingCharacter>();
            moveController.TouchEvent += MoveController_TouchEvent;
            moveController.TouchStateEvent += MoveController_TouchEvent;
        }

        // Update is called once per frame
        void Update()
        {
            if (ControlsEnabled)
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
#endif
                this.move.Move(moveDirection);

                if (Input.GetButtonDown("Jump"))
                {
                    this.jump.Jump();
                }
            } else
            {
                moveDirection = Vector2.zero;
            }
        }

        void MoveController_TouchEvent(Vector2 value)
        {
            this.moveDirection = new Vector2(value.x, 0);
        }

        void MoveController_TouchEvent(bool touchPresent)
        {
            if (!touchPresent)
            {
                this.moveDirection = Vector2.zero;
            }
        }
    }
}
