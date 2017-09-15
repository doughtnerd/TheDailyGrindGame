// Loading Screen Pro
// --------------------------------
// built by Martin Nerurkar (http://www.martin.nerurkar.de)
// of Sharkbomb Studios (http://www.sharkbombs.com)
// and Playful Systems (http://www.playful.systems)

using UnityEngine;
using UnityEngine.UI;
using PlayfulSystems.LoadingScreen;
using PlayfulSystems.ProgressBar;

public class LoadingScreenPro : LoadingScreenProBase {

    [Header("Scene Info")]
    public Text sceneInfoHeader;
    public Text sceneInfoDescription;
    public Image sceneInfoImage;
    private const string scenePreviewPath = "ScenePreviews/";

    [Header("Game Tips")]
    public Text tipHeader;
    public Text tipDescription;

    [Header("Fade Settings")]
    public bool doFade = true;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public Color fadeFromColor = Color.black;
    public Color fadeToColor = Color.black;

    CameraFade fade;

    [Header("Loading Visuals")]
    [Tooltip("A canvas group and parent for all graphics to show during loading. Leave empty if you want no fading.")]
    public CanvasGroup loadingCanvasGroup;

    [Tooltip("Progress Bar Pro that will filled as the target scene loads.")]
    public ProgressBarPro progressBar;
    [Tooltip("Fillable image that will be filled as the target scene loads.")]
    public Image loadingBar;
    public Text loadingText;
    [Tooltip("#progress# will be replaced with the loading progress from 0 to 100.")]
    public string loadingString = "#progress#%";

    [Header("Confirmation Visuals")]
    [Tooltip("A canvas group and parent for all graphics to show once loading is done. Leave empty if you want no fading.")]
    public CanvasGroup confirmationCanvasGroup;

    // -- INIT -----------------------------------------

    protected override void Init() {
        if (doFade) { 
            fade = gameObject.AddComponent<CameraFade>();
            fade.Init();
        }
    }

    // -- LOADS ALL VISUALS -----------------------------------------

    // Scene Infos

    protected override void DisplaySceneInfo(SceneInfo info) {
        SetTextIfStringIsNotNull(sceneInfoHeader, (info != null ? info.header : null));
        SetTextIfStringIsNotNull(sceneInfoDescription, (info != null ? info.description : null));

        if (sceneInfoImage != null && info != null && !System.String.IsNullOrEmpty(info.imageName)) { 
            sceneInfoImage.sprite = Resources.Load<Sprite>(scenePreviewPath + info.imageName);

            var aspectRatio = sceneInfoImage.GetComponent<AspectRatioFitter>();

            if (aspectRatio != null && sceneInfoImage.sprite != null)
                aspectRatio.aspectRatio = (sceneInfoImage.sprite.texture.width / (float)sceneInfoImage.sprite.texture.height);
        }
    }

    // Random Tips

    protected override void DisplayGameTip(LoadingTip info) {
        SetTextIfStringIsNotNull(tipHeader, (info != null ? info.header : null));
        SetTextIfStringIsNotNull(tipDescription, (info != null ? info.description : null));
    }

    // Loading Visuals

    protected override void ShowStartingVisuals() {
        if (doFade)
            fade.StartFadeFrom(fadeFromColor, fadeInDuration);
        
        #if !UNITY_5_2 && !UNITY_5_3_OR_NEWER
        // In earlier Unity versions asynchronous loading was a pro feature, so we hide the progress bar, since we can't update it
        if (CanLoadAsynchronously() == false) { 
            ShowGroup(loadingCanvasGroup, false, 0f);
            ShowGroup(confirmationCanvasGroup, false, 0f);
            return;
        }
        #endif

        SetLoadingVisuals(0f);

        ShowGroup(loadingCanvasGroup, true, 0f);
        ShowGroup(confirmationCanvasGroup, false, 0f);
    }

    protected override void ShowProgressVisuals(float progress) {
        SetLoadingVisuals(progress);
    }

    protected override void ShowLoadingDoneVisuals() {
        ShowGroup(loadingCanvasGroup, false, 0.25f);
        ShowGroup(confirmationCanvasGroup, true, 0.25f);
    }

    protected override void ShowEndingVisuals() {
        if (doFade)
            fade.StartFadeTo(fadeToColor, fadeOutDuration);
    }

    // Loading Timing

    protected override bool CanShowConfirmation() {
        if (progressBar != null)
            return !progressBar.IsAnimating();

        return true;
    }

    protected override bool CanActivateTargetScene() {
        if (doFade && fade != null)
            return !fade.IsFading();
        else
            return true;
    }

    // Util

    void SetTextIfStringIsNotNull(Text text, string s) {
        if (text == null)
            return;

        if (System.String.IsNullOrEmpty(s)) {
            text.gameObject.SetActive(false);
        }
        else {
            text.gameObject.SetActive(true);
            text.text = s;
        }
    }

    void ShowGroup(CanvasGroup group, bool show, float fadeDuration) {
        if (group == null)
            return;

        var fade = group.GetComponent<CanvasGroupFade>();

        if (fade == null)
            fade = group.gameObject.AddComponent<CanvasGroupFade>();

        if (fade != null)
            fade.FadeAlpha((show ? 0f : 1f), (show ? 1f : 0f), fadeDuration);
    }

    // Set Visuals

    void SetLoadingVisuals(float progress) {
        if (progressBar != null)
            progressBar.Value = progress;

        if (loadingBar != null)
            loadingBar.fillAmount = progress;

        if (loadingText != null)
            loadingText.text = loadingString.Replace("#progress#", Mathf.RoundToInt(progress * 100f).ToString());
    }

}