using UnityEngine;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class BottleLetterController : MonoBehaviour
    {
        [SerializeField] private Text messageText;
        [SerializeField] private float firstLetterDelaySeconds = 18f;
        [SerializeField] private float repeatDelaySeconds = 70f;
        [SerializeField] private string[] presetLetters =
        {
            "오늘은 천천히 흘러가도 괜찮아요.",
            "멀리 가지 않아도 바다는 충분히 넓어요.",
            "동반자가 말없이 곁을 지키고 있어요.",
            "작은 파도 하나도 오늘의 기록이 될 수 있어요."
        };

        private float nextLetterTime;
        private CompanionPetController companion;

        public void Initialize(Text targetMessageText, CompanionPetController companionPet)
        {
            messageText = targetMessageText;
            companion = companionPet;
            nextLetterTime = Time.time + firstLetterDelaySeconds;
        }

        /// <summary>
        /// Shows one random offline bottle letter and saves it in the album.
        /// </summary>
        public void ShowRandomLetter()
        {
            if (presetLetters == null || presetLetters.Length == 0)
            {
                return;
            }

            string letter = presetLetters[Random.Range(0, presetLetters.Length)];
            MyLittleBoatSession.Album.AddLetter(letter);
            MyLittleBoatSession.AddShells(1);
            MyLittleBoatSession.AddCompanionAffection(0.8f);

            if (messageText != null)
            {
                messageText.text = "병 속 편지\n" + letter;
            }

            if (companion != null)
            {
                companion.React("편지를 보며 가까이 다가왔어요.");
            }
        }

        private void Update()
        {
            if (Time.time < nextLetterTime)
            {
                return;
            }

            ShowRandomLetter();
            nextLetterTime = Time.time + repeatDelaySeconds;
        }
    }
}
