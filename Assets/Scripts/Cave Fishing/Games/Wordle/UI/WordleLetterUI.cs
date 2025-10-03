using Shears;
using Shears.Input;
using Shears.Tweens;
using Shears.UI;
using System;
using TMPro;
using UnityEngine;

namespace CaveFishing.Games.WordleGame.UI
{
    public class WordleLetterUI : MonoBehaviour
    {
        private const float FLIP_DELAY = 0.25f;

        [Header("Components")]
        [SerializeField] private WordleLetter letter;

        [Header("Elements")]
        [SerializeField] private ManagedUIElement button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private ManagedImage background;
        [SerializeField] private Color whiteColor;
        [SerializeField] private Color grayColor;
        [SerializeField] private Color yellowColor;
        [SerializeField] private Color greenColor;

        [Header("Tweens")]
        [SerializeField] private TweenData flipTweenData = new(0.2f);

        private Tween flipTween;
        private readonly Timer flipTimer = new();

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
            letter.Cleared += OnCleared;

            if (button != null)
                button.ClickEnded += OnButtonClicked;
        }

        private void OnDisable()
        {
            letter.LetterChanged -= OnLetterChanged;
            letter.TypeChanged -= OnTypeChanged;
            letter.Cleared -= OnCleared;

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

        private void OnTypeChanged(int delay)
        {
            if (delay == 0)
                SetFill(letter.Type);
            else
            {
                flipTimer.Start(delay * FLIP_DELAY);
                flipTimer.ClearOnCompletes();
                flipTimer.AddOnComplete(() => SetFill(letter.Type));
            }
        }

        private void OnCleared()
        {
            background.BaseColor = whiteColor;

            if (text != null)
            {
                text.color = Color.black;
                text.text = letter.Letter;
            }
        }

        public void SetFill(WordleLetter.LetterType type)
        {
            Color fillColor = whiteColor;
            Color textColor = Color.white;

            switch (type)
            {
                case WordleLetter.LetterType.Gray:
                    fillColor = grayColor;
                    textColor = Color.white;
                    break;
                case WordleLetter.LetterType.Yellow:
                    fillColor = yellowColor;
                    textColor = Color.white;
                    break;
                case WordleLetter.LetterType.Green:
                    fillColor = greenColor;
                    textColor = Color.white;
                    break;
                case WordleLetter.LetterType.None:
                    fillColor = whiteColor;
                    textColor = Color.black;
                    break;
            }

            if (type == WordleLetter.LetterType.None)
                return;

            flipTween.Dispose();

            Vector3 rotation = new(0f, 90f, 0);
            flipTween = transform.DoRotateLocalTween(Quaternion.Euler(rotation), true, flipTweenData);

            void onComplete()
            {
                background.BaseColor = fillColor;
                text.color = textColor;
                flipTween = transform.DoRotateLocalTween(Quaternion.identity, true, flipTweenData);
            }

            flipTween.AddOnComplete(onComplete);
        }
    }
}
