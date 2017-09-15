using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen {
    public class CanvasGroupFade : MonoBehaviour {

        CanvasGroup group;

        public void FadeAlpha(float fromAlpha, float toAlpha, float duration) {
            if (group == null)
                group = GetComponent<CanvasGroup>();

            if (group != null) {
                if (duration > 0f) {
                    StopAllCoroutines();
                    gameObject.SetActive(true);
                    StartCoroutine(DoFade(fromAlpha, toAlpha, duration));
                }
                else {
                    group.alpha = toAlpha;
                    gameObject.SetActive(toAlpha > 0f);
                }
            }
        }

        IEnumerator DoFade(float fromAlpha, float toAlpha, float duration) {
            float time = 0f;

            while (time < duration) {
                time += Time.deltaTime;
                group.alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);

                yield return null;
            }

            group.alpha = toAlpha;

            if (toAlpha == 0f)
                gameObject.SetActive(false);
        }
    }
}