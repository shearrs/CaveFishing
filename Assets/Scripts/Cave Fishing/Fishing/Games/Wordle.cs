using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class Wordle : MonoBehaviour
    {
        private const int MAX_GUESSES = 6;

        private readonly List<char> unguessedCharacters = new();
        private readonly List<char> guessedCharacters = new();
        private readonly List<char> yellowGuessedCharacters = new();
        private readonly List<char> greenGuessedCharacters = new();
        private readonly List<string> guesses = new();
        private string currentWord;
        private int currentGuess = 1;

        public event Action Enabled;
        public event Action<char> AddYellowCharacter;
        public event Action<char> AddGreenCharacter;
        public event Action<string> CorrectWordGuessed;
        public event Action MaxGuessesReached;

        public void Enable()
        {
            unguessedCharacters.Clear();
            guessedCharacters.Clear();
            yellowGuessedCharacters.Clear();
            greenGuessedCharacters.Clear();
            guesses.Clear();

            currentGuess = 1;
            currentWord = WordleDatabase.GetWord();

            for (char c = 'A'; c <= 'Z'; c++)
                unguessedCharacters.Add(c);

            Enabled?.Invoke();
        }

        public void SubmitGuess(string guess)
        {
            if (guess.Equals(currentWord, StringComparison.OrdinalIgnoreCase))
            {
                CorrectWordGuessed?.Invoke(guess);
                return;
            }

            foreach (var character in guess)
            {
                if (!guessedCharacters.Contains(character))
                {
                    unguessedCharacters.Remove(character);
                    guessedCharacters.Add(character);

                    if (currentWord.Contains(character) && !greenGuessedCharacters.Contains(character))
                    {
                        if (guess.IndexOf(character) == currentWord.IndexOf(character))
                        {
                            greenGuessedCharacters.Add(character);
                            AddGreenCharacter?.Invoke(character);
                        }
                        else if (!yellowGuessedCharacters.Contains(character))
                        {
                            yellowGuessedCharacters.Add(character);
                            AddYellowCharacter?.Invoke(character);
                        }
                    }
                }
            }

            currentGuess++;

            if (currentGuess > MAX_GUESSES)
                MaxGuessesReached?.Invoke();
        }
    }
}
