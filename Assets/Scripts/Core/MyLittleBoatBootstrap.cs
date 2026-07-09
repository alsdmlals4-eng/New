using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyLittleBoat
{
    public static class MyLittleBoatBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeAfterSceneLoad()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
            SceneManager.sceneLoaded += HandleSceneLoaded;
            BuildScene(SceneManager.GetActiveScene());
        }

        private static void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            BuildScene(scene);
        }

        private static void BuildScene(Scene scene)
        {
            if (scene.name == "GameScene")
            {
                if (Object.FindObjectOfType<GameSceneController>() == null)
                {
                    new GameObject("GameSceneController").AddComponent<GameSceneController>();
                }

                return;
            }

            if (Object.FindObjectOfType<MainMenuController>() == null)
            {
                new GameObject("MainMenuController").AddComponent<MainMenuController>();
            }
        }
    }
}
