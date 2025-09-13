using Shears.Input;
using Shears.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Fishing.UI
{
    public class WordleLetterUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private WordleLetter letter;

        [Header("Elements")]
        [SerializeField] private ManagedUIElement button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private ManagedImage background;
        [SerializeField] private Color grayColor;
        [SerializeField] private Color yellowColor;
        [SerializeField] private Color greenColor;

        public string Letter => letter.Letter;

        public event Action<ManagedKey> Clicked;

        private void Awake()
        {
            if (text != null)
                text.text = letter.Letter;
        }

        private void OnEnable()
        {
            letter.LetterChanged += OnLetterChanged;
            letter.TypeChanged += OnTypeChanged;

            if (button != null)
                button.ClickEnded += OnButtonClicked;
        }

        private void OnDisable()
        {
            letter.LetterChanged -= OnLetterChanged;
            letter.TypeChanged -= OnTypeChanged;

            if (button != null)
                button.ClickEnded -= OnButtonClicked;
        }

        public void Select()
        {
            button.BeginSelect();
            button.EndSelect();
        }

        private void OnButtonClicked()
        {
            Debug.Log("letter: " + Letter);
            Clicked?.Invoke(KeyTranslation.GetKey(Letter));
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
            switch (type)
            {
                case WordleLetter.LetterType.Gray:
                    background.BaseColor = grayColor;
                    break;
                case WordleLetter.LetterType.Yellow:
                    background.BaseColor = yellowColor;
                    break;
                case WordleLetter.LetterType.Green:
                    background.BaseColor = greenColor;
                    break;
                case WordleLetter.LetterType.None:
                    background.BaseColor = Color.white;
                    break;
            }
        }
    }
}
