using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class VoyageTimerController : MonoBehaviour
    {
        [SerializeField] private float voyageDurationSeconds = 300f;
        [SerializeField] private Text timerText;

        private float elapsedSeconds;
        private bool completed;
        private Action onCompleted;

        public void Initialize(float durationSeconds, Text targetTimerText, Action completedCallback)
        {
            voyageDurationSeconds = Mathf.Max(1f, durationSeconds);
            timerText = targetTimerText;
            onCompleted = completedCallback;
            elapsedSeconds = 0f;
            completed = false;
            UpdateTimerText();
        }

        /// <summary>
        /// Forces the voyage record to appear. Useful for quick manual testing in Play Mode.
        /// </summary>
        public void CompleteNowForTest()
        {
            elapsedSeconds = voyageDurationSeconds;
            CompleteVoyage();
        }

        private void Update()
        {
            if (completed)
            {
                return;
            }

            elapsedSeconds += Time.deltaTime;
            UpdateTimerText();

            if (elapsedSeconds >= voyageDurationSeconds)
            {
                CompleteVoyage();
            }
        }

        private void CompleteVoyage()
        {
            if (completed)
            {
                return;
            }

            completed = true;
            if (timerText != null)
            {
                timerText.text = "감상 모드";
            }

            if (onCompleted != null)
            {
                onCompleted.Invoke();
            }
        }

        private void UpdateTimerText()
        {
            if (timerText == null)
            {
                return;
            }

            float remaining = Mathf.Max(0f, voyageDurationSeconds - elapsedSeconds);
            int minutes = Mathf.FloorToInt(remaining / 60f);
            int seconds = Mathf.FloorToInt(remaining % 60f);
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
