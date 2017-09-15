using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayfulSystems.ProgressBar {
    public class ViewPosAnchors : ViewSizeAnchors {

		public override void UpdateView(float currentValue, float targetValue) {
			SetPivot(currentValue, currentValue);
        }
    }
}