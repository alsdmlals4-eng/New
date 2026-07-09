using UnityEngine;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class CompanionPetController : MonoBehaviour
    {
        [SerializeField] private Text statusText;

        private Renderer petRenderer;
        private Vector3 baseScale;
        private string currentReaction = "잔잔히 바다를 바라보고 있어요.";

        public void Initialize(Text companionStatusText)
        {
            statusText = companionStatusText;
            petRenderer = GetComponent<Renderer>();
            baseScale = transform.localScale;
            RefreshView();
        }

        /// <summary>
        /// Shows a simple companion reaction and updates its level-based appearance.
        /// </summary>
        public void React(string reaction)
        {
            if (!string.IsNullOrEmpty(reaction))
            {
                currentReaction = reaction;
            }

            RefreshView();
        }

        private void Update()
        {
            float bob = Mathf.Sin(Time.time * 1.8f) * 0.04f;
            transform.localScale = baseScale * (1f + MyLittleBoatSession.CompanionLevel * 0.04f);
            transform.localPosition = new Vector3(transform.localPosition.x, 0.78f + bob, transform.localPosition.z);
        }

        private void RefreshView()
        {
            int level = MyLittleBoatSession.CompanionLevel;
            if (petRenderer != null)
            {
                Color color = level == 1 ? new Color(1f, 0.92f, 0.76f) : level == 2 ? new Color(0.98f, 0.82f, 0.68f) : new Color(1f, 0.74f, 0.70f);
                petRenderer.material.color = color;
            }

            if (statusText != null)
            {
                statusText.text = "동반자 Lv " + level + "\n" + currentReaction;
            }
        }
    }
}
