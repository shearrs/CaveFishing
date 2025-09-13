using TMPro;
using UnityEngine;

namespace CaveFishing.Fishing.UI
{
    public class WordleLetterUI : MonoBehaviour
    {
        [SerializeField] private WordleLetter letter;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject grayFill;
        [SerializeField] private GameObject yellowFill;
        [SerializeField] private GameObject greenFill;

        private GameObject currentFill;

        public string Letter => letter.Letter;

        private void Awake()
        {
            if (text != null)
                text.text = letter.Letter;
        }

        private void OnEnable()
        {
            letter.LetterChanged += OnLetterChanged;
            letter.TypeChanged += OnTypeChanged;
        }

        private void OnDisable()
        {
            letter.LetterChanged -= OnLetterChanged;
            letter.TypeChanged -= OnTypeChanged;
        }

        private void OnLetterChanged()
        {
            text.text = letter.Letter;
        }

        private void OnTypeChanged()
        {
            SetFill(letter.Type);
        }

        public void SetFill(WordleLetter.LetterType type)
        {
            if (currentFill != null)
                currentFill.SetActive(false);

            switch (type)
            {
                case WordleLetter.LetterType.Gray:
                    currentFill = grayFill;
                    break;
                case WordleLetter.LetterType.Yellow:
                    currentFill = yellowFill;
                    break;
                case WordleLetter.LetterType.Green:
                    currentFill = greenFill;
                    break;
                case WordleLetter.LetterType.None:
                    currentFill = null;
                    break;
            }

            if (currentFill != null)
                currentFill.SetActive(true);
        }
    }
}
