using Shears.Input;
using Shears.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing.UI
{
    public class WordleUI : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private ManagedInputMap inputMap;

        [Header("Components")]
        [SerializeField] private Wordle wordle;
        [SerializeField] private ManagedPanel uiContainer;

        [Header("Words")]
        [SerializeField] private List<WordleLetterUI> keyboard;

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
        }

        private void OnWordleEnabled()
        {
            uiContainer.Enable();
        }

        private void OnWordleDisabled()
        {
            uiContainer.Disable();
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
    }
}
