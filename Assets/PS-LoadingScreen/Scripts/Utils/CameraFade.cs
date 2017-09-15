using UnityEngine;
using System;

namespace PlayfulSystems.LoadingScreen {
    public class CameraFade : MonoBehaviour {

        Action onFadeDone;
        const int guiDepth = -1000;

        GUIStyle backgroundStyle;
        Texture2D fadeTexture;
        Color currentColor = new Color(0, 0, 0, 0);
        Color targetColor = new Color(0, 0, 0, 0);
        Color deltaColor = new Color(0, 0, 0, 0);

        public void Init() {
            fadeTexture = new Texture2D(1, 1);
            backgroundStyle = new GUIStyle();
            backgroundStyle.normal.background = fadeTexture;
        }

        void OnGUI() {
            if (currentColor != targetColor) {
                if (Mathf.Abs(currentColor.a - targetColor.a) < Mathf.Abs(deltaColor.a) * Time.deltaTime) {
                    currentColor = targetColor;
                    SetColor(currentColor);
                    deltaColor = Color.clear;

                    if (onFadeDone != null)
                        onFadeDone();
                }
                else {
                    SetColor(currentColor + deltaColor * Time.deltaTime);
                }
            }
            if (currentColor.a > 0) {
                EnableAnim(true);
                GUI.depth = guiDepth;
                GUI.Label(new Rect(-2, -2, Screen.width + 4, Screen.height + 4), fadeTexture, backgroundStyle);
            }
            else {
                EnableAnim(false);
            }
        }

        private void SetColor(Color newColor) {
            currentColor = newColor;
            fadeTexture.SetPixel(0, 0, currentColor);
            fadeTexture.Apply();
        }

        public void StartFadeFrom(Color color, float fadeDuration, Action onFinished = null) {
            if (fadeDuration > 0.0f) {
                SetColor(color);
                onFadeDone = onFinished;
                targetColor = new Color(color.r, color.g, color.b, 0);
                SetDeltaColor(fadeDuration);
                EnableAnim(true);
            }
        }

        public void StartFadeTo(Color color, float fadeDuration, Action onFinished = null) {
            if (fadeDuration > 0.0f) {
                SetColor(new Color(color.r, color.g, color.b, 0));
                onFadeDone = onFinished;
                targetColor = color;
                SetDeltaColor(fadeDuration);
                EnableAnim(true);
            }
        }

        public void StartFadeFromTo(Color colorStart, Color colorEnd, float fadeDuration, Action onFinished = null) {
            if (fadeDuration > 0.0f) {
                SetColor(colorStart);
                onFadeDone = onFinished;
                targetColor = colorEnd;
                SetDeltaColor(fadeDuration);
                EnableAnim(true);
            }
        }

        private void EnableAnim(bool active) {
            enabled = active;
        }

        private void SetDeltaColor(float duration) {
            deltaColor = (targetColor - currentColor) / duration;
        }

        public bool IsFading() {
            if (enabled)
                return currentColor != targetColor;
            else
                return false;
        }

    }
}