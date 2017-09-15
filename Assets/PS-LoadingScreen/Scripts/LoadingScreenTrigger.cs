using UnityEngine;
using System.Collections;

public class LoadingScreenTrigger : MonoBehaviour {
	
	public enum SceneReference { Number, Name };

	public bool loadOnStart = true;
	public SceneReference loadSceneFrom;
	public int sceneNumber;
	public string sceneName;

	void Start() {
		if (loadOnStart)
			TriggerLoadScene();
	}
	
	public void TriggerLoadScene() {
		if (loadSceneFrom == SceneReference.Number)
			LoadingScreenPro.LoadScene(sceneNumber);
		else
			LoadingScreenPro.LoadScene(sceneName);
	}
	
	public void LoadScene(int number) {
		LoadingScreenPro.LoadScene(number);
	}
	
	public void LoadScene(string name) {
		LoadingScreenPro.LoadScene(name);
	}
}