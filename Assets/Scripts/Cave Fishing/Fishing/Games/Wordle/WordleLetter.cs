using System;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class WordleLetter : MonoBehaviour
    {
        public enum LetterType { None, Gray, Yellow, Green }

        [SerializeField] private string letter;
        private LetterType letterType;

        public string Letter => letter;
        public LetterType Type => letterType;

        public event Action LetterChanged;
        public event Action<int> TypeChanged;

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
    }
}
