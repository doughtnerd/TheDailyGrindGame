using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.LoadingScreen {
    [RequireComponent(typeof(Graphic))]
    public class UIGraphicPulse : MonoBehaviour {

        public Graphic gfx;

        public bool doPulse = true; 
        public Color defaultColor = Color.white;
        public Color pulseColor = Color.grey;
        public float pulseDuration = 2f;

        private bool isPulsing = false;
        private float pulseTime;

        void Update() {
            if (isPulsing != doPulse)
                SetPulsing(doPulse);

            if (!isPulsing)
                return;

            pulseTime += Time.deltaTime;
            gfx.color = Color.Lerp(defaultColor, pulseColor, GetAlpha());
        }

        void SetPulsing(bool state) {
            isPulsing = state;

            if (!isPulsing)
                gfx.color = defaultColor;
            else
                pulseTime = 0f;
        }

        float GetAlpha() {
            return 0.5f + 0.5f * Mathf.Sin( (pulseTime + pulseDuration / 4) / Mathf.PI * 20 / pulseDuration );
        }

#if UNITY_EDITOR
        private void OnValidate() {
            gfx = GetComponent<Graphic>();
        }
#endif
    }
}