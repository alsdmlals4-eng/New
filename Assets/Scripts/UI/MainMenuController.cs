using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string gameSceneName = "GameScene";

        private readonly Color panelColor = new Color(1f, 1f, 1f, 0.76f);
        private readonly Color buttonColor = new Color(0.84f, 0.95f, 0.92f, 0.92f);

        private void Start()
        {
            if (string.IsNullOrEmpty(gameSceneName))
            {
                gameSceneName = "GameScene";
            }

            BuildMenu();
        }

        /// <summary>
        /// Stores the selected mood and moves from MainMenuScene to GameScene.
        /// </summary>
        public void StartVoyage(GameMood mood)
        {
            MyLittleBoatSession.StartNewVoyage(mood);
            MyLittleBoatSession.Album.AddMoodRecord("시작 마음: " + MoodUtility.GetKoreanName(mood));
            SceneManager.LoadScene(gameSceneName);
        }

        private void BuildMenu()
        {
            Camera menuCamera = new GameObject("Menu Camera").AddComponent<Camera>();
            menuCamera.clearFlags = CameraClearFlags.SolidColor;
            menuCamera.backgroundColor = new Color(0.74f, 0.92f, 0.94f);

            Canvas canvas = SimpleUiFactory.CreateCanvas("Main Menu Canvas");
            GameObject rootPanel = SimpleUiFactory.CreatePanel(canvas.transform, "Main Menu Panel", panelColor);
            RectTransform rootRect = rootPanel.GetComponent<RectTransform>();
            SimpleUiFactory.Stretch(rootRect, new Vector2(0.05f, 0.08f), new Vector2(0.95f, 0.92f), Vector2.zero, Vector2.zero);

            VerticalLayoutGroup layout = rootPanel.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(42, 42, 48, 48);
            layout.spacing = 22f;
            layout.childControlHeight = true;
            layout.childControlWidth = true;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = true;

            Text title = SimpleUiFactory.CreateText(rootPanel.transform, "Title", "my little boat", 68, TextAnchor.MiddleLeft, new Color(0.08f, 0.18f, 0.28f));
            SimpleUiFactory.AddLayoutElement(title.gameObject, 0f, 110f);

            Text subtitle = SimpleUiFactory.CreateText(rootPanel.transform, "Subtitle", "오늘의 마음을 고르고, 동반자와 5분 동안 작은 바다를 표류합니다.", 30, TextAnchor.MiddleLeft, new Color(0.16f, 0.30f, 0.42f));
            SimpleUiFactory.AddLayoutElement(subtitle.gameObject, 0f, 96f);

            CreateMoodButton(rootPanel.transform, GameMood.Calm);
            CreateMoodButton(rootPanel.transform, GameMood.Tired);
            CreateMoodButton(rootPanel.transform, GameMood.Lonely);
            CreateMoodButton(rootPanel.transform, GameMood.Excited);
        }

        private void CreateMoodButton(Transform parent, GameMood mood)
        {
            string label = MoodUtility.GetKoreanName(mood) + "\n" + MoodUtility.GetMoodDescription(mood);
            Button button = SimpleUiFactory.CreateButton(parent, label, buttonColor);
            SimpleUiFactory.AddLayoutElement(button.gameObject, 0f, 150f);
            button.onClick.AddListener(delegate { StartVoyage(mood); });
        }
    }
}
