using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class SceneLoader : MonoBehaviour
    {

        [HideInInspector]
        public Animator animColorFade;                  //Reference to animator which will fade to and from black when starting game.

        [HideInInspector]
        public Animator animMenuAlpha;                  //Reference to animator that will fade out alpha of MenuPanel canvas group

        public AnimationClip fadeColorAnimationClip;

        public void LoadScene(int index)
        {
            animColorFade.SetTrigger("fade");
            SceneManager.LoadScene(index);
        }
    }
}
