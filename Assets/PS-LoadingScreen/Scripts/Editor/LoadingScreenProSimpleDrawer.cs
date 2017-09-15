using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_5_2 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
#endif
using PlayfulSystems.LoadingScreen;

[CustomEditor(typeof(LoadingScreenPro))]
[CanEditMultipleObjects]
public class LoadingScreenProSimpleDrawer : LoadingScreenProDrawer {

    protected SerializedProperty sceneInfoHeader;
    protected SerializedProperty sceneInfoDescription;
    protected SerializedProperty sceneInfoImage;
    protected SerializedProperty tipHeader;
    protected SerializedProperty tipDescription;

    protected override void OnEnable() {
        base.OnEnable();

        sceneInfoHeader = serializedObject.FindProperty("sceneInfoHeader");
        sceneInfoDescription = serializedObject.FindProperty("sceneInfoDescription");
        sceneInfoImage = serializedObject.FindProperty("sceneInfoImage");
        tipHeader = serializedObject.FindProperty("tipHeader");
        tipDescription = serializedObject.FindProperty("tipDescription");
    }

    protected override SerializedProperty GetLastProperty() {
        return serializedObject.FindProperty("tipDescription");
    }

    protected override void DrawFields() {
        base.DrawFields();

        EditorGUILayout.PropertyField(sceneInfoHeader);
        EditorGUILayout.PropertyField(sceneInfoDescription);
        EditorGUILayout.PropertyField(sceneInfoImage);

        if (loadingScreenPro.config != null && !loadingScreenPro.config.showSceneInfos)
            EditorGUILayout.HelpBox("Scene Infos disabled in Loading Screen Pro config.", MessageType.Info);

        EditorGUILayout.PropertyField(tipHeader);
        EditorGUILayout.PropertyField(tipDescription);

        if (loadingScreenPro.config != null && !loadingScreenPro.config.showRandomTip)
            EditorGUILayout.HelpBox("Game Tips disabled in Loading Screen Pro config.", MessageType.Info);
    }
}