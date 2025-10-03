using System;
using UnityEngine;

namespace CaveFishing.Games.WordleGame
{
    public class WordleLetter : MonoBehaviour
    {
        public enum LetterType { None, Gray, Yellow, Green }

        [SerializeField] private string letter;
        [SerializeField] private bool staticLetter;
        private LetterType letterType;

        public string Letter => letter;
        public LetterType Type => letterType;

        public event Action LetterChanged;
        public event Action<int> TypeChanged;
        public event Action Cleared;

        public void SetLetter(string text)
        {
            letter = text;

            LetterChanged?.Invoke();
        }

        public void SetType(LetterType type, int delay = 0)
        {
            letterType = type;

            TypeChanged?.Invoke(delay);
        }

        public void Clear()
        {
            if (!staticLetter)
                letter = string.Empty;

            letterType = LetterType.None;

            Cleared?.Invoke();
        }
    }
}
