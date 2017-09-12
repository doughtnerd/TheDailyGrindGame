using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

    private static GameObject instance;


	void Start()
	{
        if(instance == null)
        {
            instance = this.gameObject;
		    DontDestroyOnLoad(this.gameObject);
        } else if (instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
		//Causes UI object not to be destroyed when loading a new scene. If you want it to be destroyed, destroy it manually via script.
	}

	

}
