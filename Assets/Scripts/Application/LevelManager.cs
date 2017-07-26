using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class LevelManager : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if(currentSceneIndex+1 > SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogError("There is no next scene to load");
            } else
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }
    }
}
