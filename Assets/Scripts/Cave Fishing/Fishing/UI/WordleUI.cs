using Shears.Input;
using Shears.Logging;
using Shears.Tweens;
using Shears.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing.UI
{
    public class WordleUI : SHMonoBehaviourLogger
    {
        [Header("Input")]
        [SerializeField] private ManagedInputMap inputMap;

        [Header("Components")]
        [SerializeField] private Wordle wordle;
        [SerializeField] private ManagedPanel uiContainer;

        [Header("Words")]
        [SerializeField] private List<WordleLetterUI> keyboard;

        [Header("Tweens")]
        [SerializeField] private StructTweenData shakeTweenData = new(0.15f);

        private readonly Dictionary<string, WordleLetterUI> mappedKeyboard = new();
        private readonly List<ManagedKey> pressedKeys = new();
        private readonly List<Tween> shakeTweens = new();
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
            uiContainer.Enable();

            SubscribeToKeyboard();
            keyInput.Performed += OnKeyPressed;
        }

        private void OnWordleDisabled()
        {
            uiContainer.Disable();

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
            var currentLetters = wordle.CurrentWord.WordleLetters;

            foreach (var tween in shakeTweens)
                tween?.Dispose();

            shakeTweens.Clear();

            // THIS DOESNT WORK AS IT DOESNT SET THEIR LOCAL POSITION BACK
            // EITHER WE NEED A DICTIONARY
            // OR TWEENS NEED A SET OF CALLBACKS FOR WHEN THEY ARE STOPPED OR SOMETHING

            foreach (var letter in currentLetters)
            {
                var tween = letter.transform.DoShakeTween(10f, 0.02f, shakeTweenData);

                shakeTweens.Add(tween);
            }
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
