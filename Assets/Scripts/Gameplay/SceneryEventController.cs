using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class SceneryEventController : MonoBehaviour
    {
        private enum SceneryEventType
        {
            Sunrise,
            Sunset,
            Rain,
            Whale
        }

        [SerializeField] private Text messageText;
        [SerializeField] private float firstEventDelaySeconds = 24f;
        [SerializeField] private float repeatEventDelaySeconds = 85f;

        private float nextEventTime;
        private CompanionPetController companion;
        private Camera sceneCamera;

        public void Initialize(Text targetMessageText, CompanionPetController companionPet, Camera targetCamera)
        {
            messageText = targetMessageText;
            companion = companionPet;
            sceneCamera = targetCamera;
            nextEventTime = Time.time + firstEventDelaySeconds;
        }

        /// <summary>
        /// Triggers one simple scenery event and saves it in the scenery album.
        /// </summary>
        public void TriggerRandomEvent()
        {
            SceneryEventType eventType = (SceneryEventType)Random.Range(0, 4);
            string eventName = GetKoreanName(eventType);
            MyLittleBoatSession.Album.AddScenery(eventName);
            MyLittleBoatSession.AddShells(1);
            MyLittleBoatSession.AddCompanionAffection(0.7f);

            if (messageText != null)
            {
                messageText.text = "풍경 발견\n" + eventName;
            }

            if (companion != null)
            {
                companion.React(eventName + "을 보고 기분이 좋아졌어요.");
            }

            StartCoroutine(ShowEventObject(eventType));
        }

        private void Update()
        {
            if (Time.time < nextEventTime)
            {
                return;
            }

            TriggerRandomEvent();
            nextEventTime = Time.time + repeatEventDelaySeconds;
        }

        private IEnumerator ShowEventObject(SceneryEventType eventType)
        {
            GameObject eventRoot = new GameObject("Scenery Event - " + eventType);

            if (eventType == SceneryEventType.Whale)
            {
                CreateWhale(eventRoot.transform);
                yield return MoveEventRoot(eventRoot.transform, new Vector3(-9f, 0f, 20f), new Vector3(9f, 0f, 20f), 9f);
            }
            else if (eventType == SceneryEventType.Rain)
            {
                CreateRain(eventRoot.transform);
                yield return new WaitForSeconds(8f);
            }
            else
            {
                CreateSun(eventRoot.transform, eventType == SceneryEventType.Sunrise);
                yield return new WaitForSeconds(9f);
            }

            Destroy(eventRoot);
        }

        private IEnumerator MoveEventRoot(Transform target, Vector3 from, Vector3 to, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                if (target == null)
                {
                    yield break;
                }

                elapsed += Time.deltaTime;
                target.position = Vector3.Lerp(from, to, elapsed / duration);
                yield return null;
            }
        }

        private void CreateWhale(Transform parent)
        {
            GameObject whale = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            whale.name = "Soft Whale";
            whale.transform.SetParent(parent, false);
            whale.transform.localScale = new Vector3(2.8f, 0.8f, 1.0f);
            whale.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            whale.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Whale Material", new Color(0.30f, 0.42f, 0.55f));
        }

        private void CreateRain(Transform parent)
        {
            for (int i = 0; i < 36; i++)
            {
                GameObject drop = GameObject.CreatePrimitive(PrimitiveType.Cube);
                drop.name = "Rain Drop";
                drop.transform.SetParent(parent, false);
                drop.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(4f, 8f), Random.Range(8f, 24f));
                drop.transform.localScale = new Vector3(0.035f, 0.7f, 0.035f);
                drop.transform.localRotation = Quaternion.Euler(16f, 0f, -12f);
                drop.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Rain Material", new Color(0.78f, 0.90f, 1f, 0.72f));
            }
        }

        private void CreateSun(Transform parent, bool sunrise)
        {
            GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sun.name = sunrise ? "Sunrise" : "Sunset";
            sun.transform.SetParent(parent, false);
            sun.transform.position = new Vector3(sunrise ? -6f : 6f, 3.7f, 28f);
            sun.transform.localScale = Vector3.one * 2.2f;
            Color color = sunrise ? new Color(1f, 0.88f, 0.48f) : new Color(1f, 0.52f, 0.42f);
            sun.GetComponent<Renderer>().material = MaterialUtility.CreateColoredMaterial("Sun Event Material", color);

            if (sceneCamera != null)
            {
                sceneCamera.backgroundColor = Color.Lerp(sceneCamera.backgroundColor, color, 0.18f);
            }
        }

        private string GetKoreanName(SceneryEventType eventType)
        {
            switch (eventType)
            {
                case SceneryEventType.Sunset:
                    return "일몰";
                case SceneryEventType.Rain:
                    return "비";
                case SceneryEventType.Whale:
                    return "고래";
                default:
                    return "일출";
            }
        }
    }
}
