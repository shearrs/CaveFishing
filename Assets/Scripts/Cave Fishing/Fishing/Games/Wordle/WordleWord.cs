using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class WordleWord : MonoBehaviour
    {
        [SerializeField] private List<WordleLetter> letters;

        public string Word => GetWord();

        public void SetLetter(int index, string letter)
        {
            letters[index].SetLetter(letter);
        }

        public void SetLetterType(int index, WordleLetter.LetterType type)
        {
            letters[index].SetType(type, index);
        }

        public void AddLetter(string newLetter)
        {
            foreach (var letter in letters)
            {
                if (letter.Letter == string.Empty)
                {
                    letter.SetLetter(newLetter);
                    return;
                }
            }
        }

        public void RemoveLetter()
        {
            for (int i = letters.Count - 1; i >= 0; i--)
            {
                if (letters[i].Letter != string.Empty)
                {
                    letters[i].SetLetter(string.Empty);
                    return;
                }
            }
        }

        public string GetLetter(int index)
        {
            return letters[index].Letter;
        }

        private string GetWord()
        {
            string word = string.Empty;

            foreach (var letter in letters)
                word += letter.Letter;

            return word;
        }
    }
}
