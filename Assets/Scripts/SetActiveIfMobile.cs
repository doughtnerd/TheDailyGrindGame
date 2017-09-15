using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveIfMobile : MonoBehaviour {

    [SerializeField]
    private bool isActive;

    private void Awake()
    {

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
       
        this.gameObject.SetActive(isActive);

#endif

    }
}
