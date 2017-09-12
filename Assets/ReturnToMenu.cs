using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class ReturnToMenu : MonoBehaviour
    {

        public void LoadMenu()
        {
            ShowPanels s = GetComponent<ShowPanels>();
            Pause p = GetComponent<Pause>();
            p.UnPause();

            StateManager.Instance.SetFlag("money", 0);
            StateManager.Instance.SetFlag("promotions", 0);
            SceneManager.LoadScene(0);

            s.ShowMenu();

        }
    }
}
