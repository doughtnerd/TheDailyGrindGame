// Loading Screen Pro Base
// --------------------------------
// built by Martin Nerurkar (http://www.martin.nerurkar.de)
// of Sharkbomb Studios (http://www.sharkbombs.com)
// and Playful Systems (http://www.playful.systems)

using UnityEngine;
using System.Collections;
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using PlayfulSystems.LoadingScreen;

public abstract class LoadingScreenProBase : MonoBehaviour {

    // Stores the target scene that will be loaded from the Loading Screen
    static int targetSceneIndex = -1;
    static string targetSceneName = null;

    [Tooltip("Central Config asset.")]
    public LoadingScreenConfig config;

    [Tooltip("If loading additively, set reference to this scene's camera's audio listener, to avoid multiple active audio listeners at any one time. The script will try to auto set this for your convenience.")]
	public AudioListener audioListener;

    public enum BehaviorAfterLoad { WaitForPlayerInput, ContinueAutomatically }
    [Header("Timing Settings")]
	public BehaviorAfterLoad behaviorAfterLoad = BehaviorAfterLoad.WaitForPlayerInput;
    [Tooltip("After finishing Loading, wait this much before showing the completion visuals.")]
    public float timeToAutoContinue = 0.25f;
    
    // Private Fields
    AsyncOperation operation;
	float previousTimescale;
    float currentProgress = 0f;
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
    Scene currentScene;
#endif

    // -- TRIGGERING LOADING SCREEN -----------------------------------------

    public static void LoadScene(int levelNum) {				
        if (IsLegalLevelIndex(levelNum) == false) {
            Debug.LogWarning("No Scene with Buildindex " + levelNum + " found.");
            targetSceneName = null;
            return;
        }

        targetSceneIndex = levelNum;
        targetSceneName = null;
        LoadLoadingScene();
    }

    static bool IsLegalLevelIndex(int levelNum) {
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
        return (levelNum >= 0 || levelNum < SceneManager.sceneCountInBuildSettings);
#else
        return levelNum >= 0 || levelNum < Application.levelCount;
#endif
    }

    public static void LoadScene(string levelName) {
        targetSceneIndex = -1;
        targetSceneName = levelName;
        LoadLoadingScene();
    }

    private static void LoadLoadingScene() {
        Application.backgroundLoadingPriority = ThreadPriority.High;

#if UNITY_5_2 || UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(LoadingScreenConfig.loadingSceneName);
#else
        Application.LoadLevel(LoadingScreenConfig.loadingSceneName);
#endif
    }

    // -- LOADING SCENE BEHAVIOR -----------------------------------------

    protected virtual void Start() {
		if (targetSceneName == null && targetSceneIndex == -1 ) {
            Debug.LogWarning("[LoadingScreenPro] Directly loaded a scene with Loading Screen Pro without setting target Scene index or name.\n" +
                "Use static method LoadingScreenPro.LoadScene(int levelNum) from your scripts to use the loading screen.");
            return;
        }
        else if (config == null) {
            Debug.LogWarning("[LoadingScreenPro] Config is not set. Please open your Loading Scene and add a reference to a config file to the LoadingScreenPro and save the scene.");
            return;
        }

        previousTimescale = Time.timeScale;
		Time.timeScale = 1f; // This makes sure than the loading can progress, even if TimeScale was set to 0f
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
		currentScene = SceneManager.GetActiveScene();
#endif

        Init();

        Application.backgroundLoadingPriority = config.loadThreadPriority;
        StartCoroutine(LoadAsync(targetSceneIndex, targetSceneName));
	}

    protected virtual void Init() {
        // If you need to do any initialization in a child class, do it here. Such as ensuring certain components are present
    }

	private IEnumerator LoadAsync(int levelNum, string levelName) {
        ShowTips();
        ShowSceneInfos();
        ShowStartingVisuals();

        currentProgress = 0f;
        operation = StartOperation(levelNum, levelName);

        if (operation == null)
            yield break;

        operation.allowSceneActivation = CanLoadAdditively();

        while (IsDoneLoading() == false) {
			yield return null;
            SetProgress(operation.progress);
        }

        // If we're done, disable the audioListener on this scene, so we don't have it active for more than a frame throwing warnings
        if (CanLoadAdditively() && audioListener != null)
			audioListener.enabled = false;

        yield return null;

        SetProgress(1f);

        while (CanShowConfirmation() == false) {
            yield return null;
        }

        ShowLoadingDoneVisuals();

        if (behaviorAfterLoad == BehaviorAfterLoad.WaitForPlayerInput) {
            while (Input.anyKey == false) 
				yield return null;
		}
        else {
            yield return new WaitForSeconds(timeToAutoContinue);
        }

        ShowEndingVisuals();

        while (CanActivateTargetScene() == false) {
            yield return null;
        }

        ActivateLoadedScene();
	}

    void SetProgress(float progress) {
        if (progress <= currentProgress) 
            return;

        ShowProgressVisuals(progress);
        currentProgress = progress;
    }

	private AsyncOperation StartOperation(int levelNum, string levelName) {
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
        LoadSceneMode mode = CanLoadAdditively() ? LoadSceneMode.Additive : LoadSceneMode.Single;

        if (System.String.IsNullOrEmpty(levelName))
            return SceneManager.LoadSceneAsync(levelNum, mode);
        else
            return SceneManager.LoadSceneAsync(levelName, mode);
#else
        if (CanLoadAsynchronously()) { 
            if (System.String.IsNullOrEmpty(levelName)) 
                return Application.LoadLevelAsync(levelNum);
            else 
                return Application.LoadLevelAsync(levelName);
        }
        else {
            if (System.String.IsNullOrEmpty(levelName))
                Application.LoadLevel(levelNum);
            else
                Application.LoadLevel(levelName);

            return null;
        }
#endif
    }

    private bool CanLoadAdditively() {
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
        return config.loadAdditively;
#else
        return false;
#endif
    }

    protected bool CanLoadAsynchronously() {
#if UNITY_5 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3_OR_NEWER
        return true;
#else
        return Application.HasProLicense();
#endif
    }

    private bool IsDoneLoading() {
        // operation on "single mode" does not auto-activate scene, so it's stuck at 0.9
        if (CanLoadAdditively())
            return operation.isDone;
        else
            return operation.progress >= 0.9f;
    }

	// Loads the scene
	private void ActivateLoadedScene() {
        targetSceneIndex = -1;
        targetSceneName = null;

		if (CanLoadAdditively()) {
#if UNITY_5_5_OR_NEWER
            SceneManager.UnloadSceneAsync(currentScene);
#elif UNITY_5
            SceneManager.UnloadScene(currentScene);
#endif
        }

        operation.allowSceneActivation = true;
        Resources.UnloadUnusedAssets();
        Time.timeScale = previousTimescale;
	}


    // -- LOADS ALL VISUALS -----------------------------------------

    // Scene Infos

    void ShowSceneInfos() {
        if (config.showSceneInfos == false || config.sceneInfos == null || config.sceneInfos.Length == 0)
            DisplaySceneInfo(null);
        else
            DisplaySceneInfo(config.GetSceneInfo(targetSceneName));
    }

    protected virtual void DisplaySceneInfo(SceneInfo info) {
    }

    // Random Tips

    void ShowTips() {
        if (config.showRandomTip == false || config.gameTips == null || config.gameTips.Length == 0)
            DisplayGameTip(null);
        else
            DisplayGameTip(config.GetGameTip());
    }

    protected virtual void DisplayGameTip(LoadingTip tip) {
    }

    // Loading Screen States

    protected virtual void ShowStartingVisuals() {
    }

    protected virtual void ShowProgressVisuals(float progress) {
	}

    protected virtual void ShowLoadingDoneVisuals() {
    }

    protected virtual void ShowEndingVisuals() {
    }

    // Loading Timing

    protected virtual bool CanShowConfirmation() {
        return true;
    }

    protected virtual bool CanActivateTargetScene() {
        return true;
    }

#if UNITY_EDITOR

    // This ensures that the AudioListener value is set to the main camera of the Loading Scene, if possible
    protected virtual void Reset() {
        DetectAudioListener();
    }

    public void DetectAudioListener() {
        if (Camera.main != null)
            audioListener = Camera.main.GetComponent<AudioListener>();
    }

    // Used to check if this is the current scene when editing the LoadingScreenPro
    public string GetLoadingSceneName() {
        return LoadingScreenConfig.loadingSceneName;
    }

#endif

}