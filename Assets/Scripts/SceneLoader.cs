using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Animator animColorFade;

        public AnimationClip fadeColorAnimationClip;

        public static SceneLoader Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void LoadScene(int index)
        {
            StartCoroutine(LoadSceneDelayed(1, index));
        }

        private IEnumerator LoadSceneDelayed(float delay, int index)
        {
            //animColorFade.SetTrigger("fade");
            //yield return new WaitForSeconds(delay);
            //SceneManager.LoadSceneAsync(index);
            StateManager.Instance.SetFlag("money", 0);
            StateManager.Instance.SetFlag("promotions", 0);
            LoadingScreenPro.LoadScene(index);
            yield return null;
        }
    }
}
