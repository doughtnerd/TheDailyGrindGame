using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class PauseHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject pausePanel;

        private bool isPaused = false;

        private void Awake()
        {
            pausePanel.SetActive(false);
        }

        void Update()
        {

            //Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
            if (Input.GetButtonDown("Cancel") && !isPaused)
            {
                //Call the DoPause function to pause the game
                DoPause();
            }
            //If the button is pressed and the game is paused and not in main menu
            else if (Input.GetButtonDown("Cancel") && isPaused)
            {
                //Call the UnPause function to unpause the game
                UnPause();
            }

        }

        public void DoPause()
        {
            //Set isPaused to true
            isPaused = true;
            //Set time.timescale to 0, this will cause animations and physics to stop updating
            Time.timeScale = 0;

            this.pausePanel.SetActive(true);
        }

        public void UnPause()
        {
            isPaused = false;
            Time.timeScale = 1;
            this.pausePanel.SetActive(false);

        }
    }
}
