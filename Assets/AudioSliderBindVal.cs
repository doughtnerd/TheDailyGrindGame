using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliderBindVal : MonoBehaviour {

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private string mixerField;

    private Slider slider;

	// Use this for initialization
	void Start () {
        this.slider = GetComponent<Slider>();
        float val;
        if(this.mixer.GetFloat(this.mixerField, out val))
        {
            this.slider.value = val;
        }
	}
}
