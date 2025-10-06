using Shears;
using Shears.Input;
using Shears.Logging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame
{
    public class Keyboard : SHMonoBehaviourLogger
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private int maxLetters = 15;
        [SerializeField, ReadOnly] private string inputLetters;

        private readonly List<ManagedKey> pressedKeys = new();
        private IManagedInput keyInput;
        private bool isEnabled = false;

        public event Action<string> InputLettersUpdated;

        private void Awake()
        {
            keyInput = inputProvider.GetInput("Key");
        }

        private void OnDisable()
        {
            keyInput.Performed -= OnKeyInput;
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            Clear();

            keyInput.Performed += OnKeyInput;

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            keyInput.Performed -= OnKeyInput;

            isEnabled = false;
        }

        public void Clear()
        {
            inputLetters = string.Empty;
            InputLettersUpdated?.Invoke(inputLetters);
        }

        private void OnKeyInput(ManagedInputInfo info)
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
                if (inputLetters.Length > 0)
                    inputLetters = inputLetters[..^1];
            }
            else if (key.IsLetter() && inputLetters.Length < maxLetters)
                inputLetters += key.GetDisplayName().ToLower();

            InputLettersUpdated?.Invoke(inputLetters);
        }
    }
}
