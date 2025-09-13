using Shears.Input;
using Shears.Logging;
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

        private readonly Dictionary<string, WordleLetterUI> mappedKeyboard = new();
        private readonly List<ManagedKey> pressedKeys = new();
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

            wordle.GrayCharacterAdded += OnGrayCharacterAdded;
            wordle.YellowCharacterAdded += OnYellowCharacterAdded;
            wordle.GreenCharacterAdded += OnGreenCharacterAdded;
        }

        private void OnDisable()
        {
            wordle.Enabled -= OnWordleEnabled;
            wordle.Disabled -= OnWordleDisabled;

            wordle.GrayCharacterAdded -= OnGrayCharacterAdded;
            wordle.YellowCharacterAdded -= OnYellowCharacterAdded;
            wordle.GreenCharacterAdded -= OnGreenCharacterAdded;

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

        private void OnGrayCharacterAdded(string character)
        {
            foreach (var letter in keyboard)
            {
                if (letter.Letter == character)
                    letter.SetFill(WordleLetter.LetterType.Gray);
            }
        }

        private void OnYellowCharacterAdded(string character)
        {
            foreach (var letter in keyboard)
            {
                if (letter.Letter == character)
                    letter.SetFill(WordleLetter.LetterType.Yellow);
            }
        }

        private void OnGreenCharacterAdded(string character)
        {
            foreach (var letter in keyboard)
            {
                if (letter.Letter == character)
                    letter.SetFill(WordleLetter.LetterType.Green);
            }
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
