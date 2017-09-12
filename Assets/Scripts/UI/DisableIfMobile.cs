using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class DisableIfMobile : MonoBehaviour
    {

        private void Awake()
        {

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
       
        this.gameObject.SetActive(false);

#endif

        }
    }
}
