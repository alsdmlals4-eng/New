using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField] private float voyageDurationSeconds = 300f;

        private readonly List<Transform> driftObjects = new List<Transform>();
        private readonly Color panelColor = new Color(1f, 1f, 1f, 0.70f);
        private readonly Color buttonColor = new Color(0.86f, 0.94f, 0.92f, 0.96f);

        private Camera sceneCamera;
        private Canvas gameCanvas;
        private CanvasGroup hudCanvasGroup;
        private Text timerText;
        private Text statusText;
        private Text speedText;
        private Text companionText;
        private Text messageText;
        private GameObject recordPanel;
        private Text recordText;
        private GameObject photoFlashPanel;

        private OceanDriftController oceanController;
        private CompanionPetController companionController;
        private AlbumUiController albumUiController;
        private bool appreciationMode;
        private TimePeriod currentPeriod;

        private void Start()
        {
            currentPeriod = TimeOfDayService.GetCurrentPeriod();
            BuildWorld();
            BuildHud();
            ConnectGameplayControllers();
            ShowMessage("오늘의 바다", "드래그로 주변을 둘러보고, 사진과 편지를 천천히 모아보세요.");
        }

        /// <summary>
        /// Saves a simple photo record to the album and gives the companion a small reaction.
        /// </summary>
        public void TakePhoto()
        {
            string photoRecord = DateTime.Now.ToString("HH:mm") + " - " + MoodUtility.GetKoreanName(MyLittleBoatSession.SelectedMood) + "의 " + TimeOfDayService.GetKoreanName(currentPeriod);
            MyLittleBoatSession.Album.AddPhoto(photoRecord);
            MyLittleBoatSession.AddShells(1);
            MyLittleBoatSession.AddCompanionAffection(0.45f);

            if (companionController != null)
            {
                companionController.React("찰칵 소리에 기분 좋게 흔들렸어요.");
            }

            ShowMessage("사진 저장", "사진 앨범에 기록했어요.");
            StartCoroutine(FlashPhotoPanel());
            RefreshStatusText();
        }

        /// <summary>
        /// Lowers the HUD opacity so the player can quietly watch the sea.
        /// </summary>
        public void ToggleAppreciationMode()
        {
            appreciationMode = !appreciationMode;
            if (hudCanvasGroup != null)
            {
                hudCanvasGroup.alpha = appreciationMode ? 0.24f : 1f;
            }

            ShowMessage("감상모드", appreciationMode ? "UI를 낮추고 바다를 더 넓게 보여줍니다." : "기본 UI로 돌아왔어요.");
        }

        /// <summary>
        /// Applies one of the three MVP drift speeds: slow, normal, or fast.
        /// </summary>
        public void SetDriftSpeed(DriftSpeed speed)
        {
            if (oceanController != null)
            {
                oceanController.SetSpeed(speed);
            }

            if (speedText != null)
            {
                speedText.text = "속도: " + GetSpeedName(speed);
            }
        }

        private void BuildWorld()
        {
            GameObject cameraObject = new GameObject("First Person Boat Camera", typeof(Camera), typeof(AudioListener), typeof(FirstPersonLook));
            sceneCamera = cameraObject.GetComponent<Camera>();
            sceneCamera.transform.position = new Vector3(0f, 1.42f, -3.2f);
            sceneCamera.transform.rotation = Quaternion.Euler(6f, 0f, 0f);
            sceneCamera.fieldOfView = 64f;
            TimeOfDayService.ApplyCameraBackground(sceneCamera, MyLittleBoatSession.SelectedMood);

            GameObject lightObject = new GameObject("Soft Sun Light", typeof(Light));
            Light light = lightObject.GetComponent<Light>();
            light.type = LightType.Directional;
            light.color = currentPeriod == TimePeriod.MorningSea ? new Color(1f, 0.92f, 0.76f) : new Color(0.60f, 0.72f, 1f);
            light.intensity = currentPeriod == TimePeriod.MorningSea ? 1.05f : 0.55f;
            lightObject.transform.rotation = Quaternion.Euler(45f, -30f, 0f);

            CreateOcean();
            CreateBoatAndCompanion();
            CreateDriftObjects();
            CreateNightStarsIfNeeded();
        }

        private void CreateOcean()
        {
            GameObject ocean = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ocean.name = "Pastel Ocean Mesh";
            ocean.transform.position = new Vector3(0f, -0.05f, 18f);
            ocean.transform.localScale = new Vector3(8f, 1f, 8f);
            ocean.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Ocean Material", MoodUtility.GetSeaColor(MyLittleBoatSession.SelectedMood, currentPeriod));

            for (int i = 0; i < 18; i++)
            {
                GameObject wave = GameObject.CreatePrimitive(PrimitiveType.Cube);
                wave.name = "Soft Wave Line";
                wave.transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0.02f, UnityEngine.Random.Range(6f, 42f));
                wave.transform.localScale = new Vector3(UnityEngine.Random.Range(1.4f, 3.4f), 0.025f, 0.045f);
                wave.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Wave Line Material", new Color(1f, 1f, 1f, 0.62f));
                driftObjects.Add(wave.transform);
            }
        }

        private void CreateBoatAndCompanion()
        {
            GameObject boatRoot = new GameObject("Visible Boat Front");

            GameObject bow = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bow.name = "Small Boat Bow";
            bow.transform.SetParent(boatRoot.transform, false);
            bow.transform.position = new Vector3(0f, 0.32f, -0.55f);
            bow.transform.localScale = new Vector3(2.8f, 0.28f, 3.4f);
            bow.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Warm Boat Wood", new Color(1f, 0.75f, 0.44f));

            GameObject rim = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rim.name = "White Boat Rim";
            rim.transform.SetParent(boatRoot.transform, false);
            rim.transform.position = new Vector3(0f, 0.45f, -0.55f);
            rim.transform.localScale = new Vector3(3.1f, 0.14f, 3.7f);
            rim.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Boat Rim", new Color(0.94f, 0.98f, 1f));

            GameObject companion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            companion.name = "동반자";
            companion.transform.SetParent(boatRoot.transform, false);
            companion.transform.localPosition = new Vector3(0.82f, 0.78f, 0.25f);
            companion.transform.localScale = new Vector3(0.42f, 0.36f, 0.42f);
            companion.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Companion Material", new Color(1f, 0.92f, 0.76f));
            companionController = companion.AddComponent<CompanionPetController>();
        }

        private void CreateDriftObjects()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject objectOnSea = GameObject.CreatePrimitive(i % 3 == 0 ? PrimitiveType.Capsule : PrimitiveType.Cube);
                objectOnSea.name = i % 3 == 0 ? "Bottle Shape" : "Soft Floating Debris";
                objectOnSea.transform.position = new Vector3(UnityEngine.Random.Range(-8f, 8f), 0.18f, UnityEngine.Random.Range(12f, 46f));
                objectOnSea.transform.localScale = i % 3 == 0 ? new Vector3(0.18f, 0.42f, 0.18f) : new Vector3(0.5f, 0.08f, 0.24f);
                objectOnSea.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Floating Object Material", new Color(0.95f, 0.86f, 0.68f));
                driftObjects.Add(objectOnSea.transform);
            }
        }

        private void CreateNightStarsIfNeeded()
        {
            if (currentPeriod != TimePeriod.NightSea)
            {
                return;
            }

            for (int i = 0; i < 32; i++)
            {
                GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                star.name = "Quiet Star";
                star.transform.position = new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(6f, 11f), UnityEngine.Random.Range(16f, 42f));
                star.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.025f, 0.06f);
                star.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Star Material", new Color(1f, 0.96f, 0.78f));
            }
        }

        private void BuildHud()
        {
            gameCanvas = SimpleUiFactory.CreateCanvas("Game HUD Canvas");
            hudCanvasGroup = gameCanvas.gameObject.AddComponent<CanvasGroup>();

            GameObject topPanel = SimpleUiFactory.CreatePanel(gameCanvas.transform, "Top Status Panel", panelColor);
            SimpleUiFactory.Stretch(topPanel.GetComponent<RectTransform>(), new Vector2(0.03f, 0.91f), new Vector2(0.97f, 0.985f), Vector2.zero, Vector2.zero);
            HorizontalLayoutGroup topLayout = topPanel.AddComponent<HorizontalLayoutGroup>();
            topLayout.padding = new RectOffset(20, 20, 10, 10);
            topLayout.spacing = 14f;
            topLayout.childForceExpandWidth = true;
            topLayout.childControlHeight = true;
            topLayout.childControlWidth = true;

            statusText = SimpleUiFactory.CreateText(topPanel.transform, "Status Text", string.Empty, 28, TextAnchor.MiddleLeft, new Color(0.08f, 0.18f, 0.28f));
            timerText = SimpleUiFactory.CreateText(topPanel.transform, "Timer Text", "05:00", 34, TextAnchor.MiddleCenter, new Color(0.08f, 0.18f, 0.28f));
            companionText = SimpleUiFactory.CreateText(topPanel.transform, "Companion Text", "동반자 Lv 1", 25, TextAnchor.MiddleRight, new Color(0.08f, 0.18f, 0.28f));
            SimpleUiFactory.AddLayoutElement(timerText.gameObject, 180f, 0f);

            GameObject messagePanel = SimpleUiFactory.CreatePanel(gameCanvas.transform, "Message Panel", new Color(1f, 1f, 1f, 0.65f));
            SimpleUiFactory.Stretch(messagePanel.GetComponent<RectTransform>(), new Vector2(0.07f, 0.72f), new Vector2(0.93f, 0.84f), Vector2.zero, Vector2.zero);
            messageText = SimpleUiFactory.CreateText(messagePanel.transform, "Message Text", string.Empty, 30, TextAnchor.MiddleCenter, new Color(0.10f, 0.20f, 0.31f));
            SimpleUiFactory.Stretch(messageText.GetComponent<RectTransform>(), Vector2.zero, Vector2.one, new Vector2(22f, 12f), new Vector2(-22f, -12f));

            GameObject bottomPanel = SimpleUiFactory.CreatePanel(gameCanvas.transform, "Bottom Controls Panel", panelColor);
            SimpleUiFactory.Stretch(bottomPanel.GetComponent<RectTransform>(), new Vector2(0.03f, 0.025f), new Vector2(0.97f, 0.18f), Vector2.zero, Vector2.zero);
            HorizontalLayoutGroup bottomLayout = bottomPanel.AddComponent<HorizontalLayoutGroup>();
            bottomLayout.padding = new RectOffset(16, 16, 14, 14);
            bottomLayout.spacing = 10f;
            bottomLayout.childForceExpandWidth = true;
            bottomLayout.childControlWidth = true;
            bottomLayout.childControlHeight = true;

            CreateHudButton(bottomPanel.transform, "사진찍기", TakePhoto);
            CreateHudButton(bottomPanel.transform, "감상모드", ToggleAppreciationMode);
            CreateHudButton(bottomPanel.transform, "앨범", ToggleAlbum);
            CreateHudButton(bottomPanel.transform, "느림", delegate { SetDriftSpeed(DriftSpeed.Slow); });
            CreateHudButton(bottomPanel.transform, "보통", delegate { SetDriftSpeed(DriftSpeed.Normal); });
            CreateHudButton(bottomPanel.transform, "빠름", delegate { SetDriftSpeed(DriftSpeed.Fast); });

            speedText = SimpleUiFactory.CreateText(bottomPanel.transform, "Speed Text", "속도: 보통", 26, TextAnchor.MiddleCenter, new Color(0.08f, 0.18f, 0.28f));
            SimpleUiFactory.AddLayoutElement(speedText.gameObject, 170f, 0f);

            CreateRecordPanel();
            CreatePhotoFlashPanel();
            RefreshStatusText();
        }

        private void CreateRecordPanel()
        {
            recordPanel = SimpleUiFactory.CreatePanel(gameCanvas.transform, "Voyage Record Panel", new Color(1f, 1f, 1f, 0.92f));
            SimpleUiFactory.Stretch(recordPanel.GetComponent<RectTransform>(), new Vector2(0.08f, 0.28f), new Vector2(0.92f, 0.72f), Vector2.zero, Vector2.zero);

            VerticalLayoutGroup layout = recordPanel.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(34, 34, 34, 34);
            layout.spacing = 18f;
            layout.childControlHeight = true;
            layout.childControlWidth = true;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = true;

            recordText = SimpleUiFactory.CreateText(recordPanel.transform, "Voyage Record Text", string.Empty, 34, TextAnchor.MiddleCenter, new Color(0.08f, 0.18f, 0.28f));
            SimpleUiFactory.AddLayoutElement(recordText.gameObject, 0f, 310f);

            Button closeButton = SimpleUiFactory.CreateButton(recordPanel.transform, "계속 감상하기", buttonColor);
            closeButton.onClick.AddListener(delegate { recordPanel.SetActive(false); });
            SimpleUiFactory.AddLayoutElement(closeButton.gameObject, 0f, 82f);
            recordPanel.SetActive(false);
        }

        private void CreatePhotoFlashPanel()
        {
            photoFlashPanel = SimpleUiFactory.CreatePanel(gameCanvas.transform, "Photo Flash", new Color(1f, 1f, 1f, 0.86f));
            SimpleUiFactory.Stretch(photoFlashPanel.GetComponent<RectTransform>(), Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            photoFlashPanel.SetActive(false);
        }

        private void ConnectGameplayControllers()
        {
            oceanController = gameObject.AddComponent<OceanDriftController>();
            oceanController.Initialize(driftObjects);
            oceanController.SetSpeed(DriftSpeed.Normal);

            if (companionController != null)
            {
                companionController.Initialize(companionText);
            }

            VoyageTimerController timerController = gameObject.AddComponent<VoyageTimerController>();
            timerController.Initialize(voyageDurationSeconds, timerText, ShowVoyageRecord);

            BottleLetterController letterController = gameObject.AddComponent<BottleLetterController>();
            letterController.Initialize(messageText, companionController);

            SceneryEventController sceneryController = gameObject.AddComponent<SceneryEventController>();
            sceneryController.Initialize(messageText, companionController, sceneCamera);

            albumUiController = gameObject.AddComponent<AlbumUiController>();
            albumUiController.Initialize(gameCanvas.transform);
        }

        private void CreateHudButton(Transform parent, string label, UnityEngine.Events.UnityAction action)
        {
            Button button = SimpleUiFactory.CreateButton(parent, label, buttonColor);
            button.onClick.AddListener(action);
            SimpleUiFactory.AddLayoutElement(button.gameObject, 0f, 96f);
        }

        private void ToggleAlbum()
        {
            if (albumUiController != null)
            {
                albumUiController.ToggleAlbum();
            }
        }

        private void ShowVoyageRecord()
        {
            string moodName = MoodUtility.GetKoreanName(MyLittleBoatSession.SelectedMood);
            string periodName = TimeOfDayService.GetKoreanName(currentPeriod);
            string record = "오늘의 항해 기록\n" + moodName + "의 " + periodName + "에서 5분을 머물렀어요.\n사진 " + MyLittleBoatSession.Album.Photos.Count + "장, 풍경 " + MyLittleBoatSession.Album.Scenery.Count + "개, 편지 " + MyLittleBoatSession.Album.Letters.Count + "개";
            MyLittleBoatSession.Album.AddMoodRecord(record);
            MyLittleBoatSession.AddShells(5);
            MyLittleBoatSession.AddCompanionAffection(1.1f);

            if (recordText != null)
            {
                recordText.text = record;
            }

            if (recordPanel != null)
            {
                recordPanel.SetActive(true);
            }

            if (companionController != null)
            {
                companionController.React("오늘의 항해가 끝나자 편안히 곁에 앉았어요.");
            }

            RefreshStatusText();
        }

        private IEnumerator FlashPhotoPanel()
        {
            if (photoFlashPanel == null)
            {
                yield break;
            }

            photoFlashPanel.SetActive(true);
            yield return new WaitForSeconds(0.16f);
            photoFlashPanel.SetActive(false);
        }

        private void ShowMessage(string title, string body)
        {
            if (messageText != null)
            {
                messageText.text = title + "\n" + body;
            }
        }

        private void RefreshStatusText()
        {
            if (statusText == null)
            {
                return;
            }

            statusText.text = "my little boat\n마음: " + MoodUtility.GetKoreanName(MyLittleBoatSession.SelectedMood) + " / " + TimeOfDayService.GetKoreanName(currentPeriod) + " / 조개 " + MyLittleBoatSession.ShellCount;
        }

        private string GetSpeedName(DriftSpeed speed)
        {
            if (speed == DriftSpeed.Slow)
            {
                return "느림";
            }

            if (speed == DriftSpeed.Fast)
            {
                return "빠름";
            }

            return "보통";
        }
    }
}

