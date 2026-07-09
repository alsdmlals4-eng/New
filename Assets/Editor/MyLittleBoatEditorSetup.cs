using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace MyLittleBoat.EditorTools
{
    /// <summary>
    /// Creates the two MVP scenes and keeps them in Build Settings when the project opens.
    /// </summary>
    [InitializeOnLoad]
    public static class MyLittleBoatEditorSetup
    {
        private const string MainMenuScenePath = "Assets/Scenes/MainMenuScene.unity";
        private const string GameScenePath = "Assets/Scenes/GameScene.unity";

        static MyLittleBoatEditorSetup()
        {
            EditorApplication.delayCall += EnsureProjectSetup;
        }

        [MenuItem("my little boat/Setup MVP Scenes")]
        public static void EnsureProjectSetup()
        {
            Directory.CreateDirectory("Assets/Scenes");
            EnsureScene(MainMenuScenePath);
            EnsureScene(GameScenePath);

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(MainMenuScenePath, true),
                new EditorBuildSettingsScene(GameScenePath, true)
            };
        }

        private static void EnsureScene(string path)
        {
            if (File.Exists(path))
            {
                return;
            }

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, path);
        }
    }
}
