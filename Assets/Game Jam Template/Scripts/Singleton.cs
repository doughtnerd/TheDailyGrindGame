using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour {

    public static Singleton<T> SingletonObject { get; private set; }

	// Use this for initialization
	void Awake () {
        if (SingletonObject == null)
        {
            SingletonObject = this;
        } else if (SingletonObject != this)
        {
            Destroy(this);
        }
	}
}
