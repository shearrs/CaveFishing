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
        [SerializeField] private List<WordleWord> words;
        [SerializeField] private List<WordleLetter> keyboard;

        private IManagedInput keyInput;

        private void Awake()
        {
            keyInput = inputMap.GetInput("Key");
        }

        private void OnEnable()
        {
            wordle.Enabled += OnWordleEnabled;
            keyInput.Performed += OnKeyPressed;
        }

        private void OnDisable()
        {
            wordle.Enabled -= OnWordleEnabled;
            keyInput.Performed -= OnKeyPressed;
        }

        private void OnWordleEnabled()
        {
            uiContainer.Enable();
        }

        private void OnKeyPressed(ManagedInputInfo info)
        {
            Debug.Log(keyInput.ReadValue<KeyControl>());
        }
    }
}
