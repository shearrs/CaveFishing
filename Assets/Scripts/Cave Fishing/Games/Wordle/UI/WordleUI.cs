using Shears.Input;
using Shears.Logging;
using Shears.Tweens;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.WordleGame.UI
{
    public class WordleUI : SHMonoBehaviourLogger
    {
        [Header("Input")]
        [SerializeField] private ManagedInputMap inputMap;

        [Header("Components")]
        [SerializeField] private Wordle wordle;
        [SerializeField] private Canvas gameCanvas;

        [Header("Words")]
        [SerializeField] private List<WordleLetterUI> keyboard;

        [Header("Tweens")]
        [SerializeField] private StructTweenData shakeTweenData = new(0.15f);

        private readonly Dictionary<string, WordleLetterUI> mappedKeyboard = new();
        private readonly List<ManagedKey> pressedKeys = new();
        private Tween shakeTween;
        private IManagedInput keyInput;

        private void Awake()
        {
            keyInput = inputMap.GetInput("Key");

            foreach (var key in keyboard)
                mappedKeyboard[key.Letter] = key;
        }

        private void OnEnable()
        {
            wordle.Enabled += OnWordleEnabled;
            wordle.Disabled += OnWordleDisabled;
            wordle.InvalidWordSubmitted += OnInvalidWordSubmitted;
        }

        private void OnDisable()
        {
            wordle.Enabled -= OnWordleEnabled;
            wordle.Disabled -= OnWordleDisabled;
            wordle.InvalidWordSubmitted -= OnInvalidWordSubmitted;

            keyInput.Performed -= OnKeyPressed;
        }

        private void OnWordleEnabled()
        {
            gameCanvas.enabled = true;

            SubscribeToKeyboard();
            keyInput.Performed += OnKeyPressed;
        }

        private void OnWordleDisabled()
        {
            gameCanvas.enabled = false;

            UnsubscribeFromKeyboard();
            keyInput.Performed -= OnKeyPressed;
        }

        private void SubscribeToKeyboard()
        {
            foreach (var key in keyboard)
                key.Clicked += ProcessKey;
        }

        private void UnsubscribeFromKeyboard()
        {
            foreach (var key in keyboard)
                key.Clicked -= ProcessKey;
        }

        private void OnInvalidWordSubmitted()
        {
            var word = wordle.CurrentWord;

            if (shakeTween != null && shakeTween.IsPlaying)
                return;

            shakeTween = word.transform.DoShakeTween(5, 0.02f, shakeTweenData);
        }

        private void OnKeyPressed(ManagedInputInfo info)
        {
            ManagedKeyboard.GetKeysPressedThisFrame(pressedKeys);

            if (pressedKeys.Count == 0)
                return;

            Log("Key pressed: " + pressedKeys[0].GetDisplayName(), SHLogLevels.Verbose);

            foreach (var key in pressedKeys)
                ProcessKey(key);
        }

        private void ProcessKey(ManagedKey key)
        {
            if (key == ManagedKey.Backspace)
            {
                wordle.RemoveLetter();
                mappedKeyboard["BACKSPACE"].Select();
            }
            else if (key == ManagedKey.Enter && wordle.CurrentWordLength == 5)
            {
                wordle.SubmitGuess();
                mappedKeyboard["ENTER"].Select();
            }
            else if (key.IsLetter())
            {
                mappedKeyboard[key.GetDisplayName()].Select();
                wordle.AddLetter(key.GetDisplayName());
            }
        }
    }
}
