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
        [SerializeField, ReadOnly] private string inputLetters;

        private IManagedInput keyInput;
        private readonly List<ManagedKey> pressedKeys = new();

        public event Action<string> InputLettersUpdated;

        private void Awake()
        {
            keyInput = inputProvider.GetInput("Key");
        }

        private void OnEnable()
        {
            keyInput.Performed += OnKeyInput;
        }

        private void OnDisable()
        {
            keyInput.Performed -= OnKeyInput;
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

                return;
            }
            else if (!key.IsLetter())
                return;

            inputLetters += key.GetDisplayName();

            InputLettersUpdated?.Invoke(inputLetters);
        }
    }
}
