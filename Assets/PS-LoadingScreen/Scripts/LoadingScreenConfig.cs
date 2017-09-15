using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen {
    public class LoadingScreenConfig : ScriptableObject {

        // ----------------------------------------------------------------------------------------------------------
        // IMPORTANT!
        //
        // This is the NAME of your loading scene. Change it to match your scene.
        // Make sure that the loading scene is also added to your build settings.
        //
        // !!!EDIT HERE!!!
        // ----------------------------------------------------------------------------------------------------------
        static public string loadingSceneName = "PS-LoadingScene_1";

        
        [Header("Loading Behavior")]
        [Tooltip("Loading additively means that the scene is loaded in the background in addition to the loading scene and is then turned off as the loading scene is unloaded.")]
        public bool loadAdditively = false;

        [Tooltip("Lower priority means a background operation will run less often and will take up less time, but will progress more slowly.")]
        public ThreadPriority loadThreadPriority;

        [Header("Scene Infos")]
        public bool showSceneInfos = false;
        public SceneInfo[] sceneInfos;

        [Header("Game Tips")]
        public bool showRandomTip = true;
        public LoadingTip[] gameTips;

        public virtual SceneInfo GetSceneInfo(string targetSceneName) {
            for (int i = 0; i < sceneInfos.Length; i++)
                if (sceneInfos[i].sceneName == targetSceneName)
                    return sceneInfos[i];

            return null;
        }

        public virtual LoadingTip GetGameTip() {
            return gameTips[Random.Range(0, gameTips.Length)];
        }
    }

    [System.Serializable]
    public class SceneInfo {
        public string sceneName;
        [Tooltip("Images are loaded from Resources/ScenePreviews/. Leave empty to keep default background in Loading Scene.")]
        public string imageName;
        public string header;
        public string description;
    }

    [System.Serializable]
    public class LoadingTip {
        public string header;
        public string description;
    }
}