using UnityEngine;
using System.Collections;
using PlayfulSystems.LoadingScreen;

public class LoadSceneButton : MonoBehaviour {

    public string targetSceneName = "TestScene_0";

    public void SetLoadingScene(string sceneName) {
        PlayfulSystems.LoadingScreen.LoadingScreenConfig.loadingSceneName = sceneName;
        var fade = gameObject.AddComponent<CameraFade>();
        fade.Init();

        fade.StartFadeTo(Color.black, 1f, LoadTargetScene);
    }

    void LoadTargetScene() {
        LoadingScreenPro.LoadScene(targetSceneName);
    }
}
