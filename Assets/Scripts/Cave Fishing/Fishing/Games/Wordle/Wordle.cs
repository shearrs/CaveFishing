using Shears;
using Shears.Input;
using Shears.Logging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class Wordle : SHMonoBehaviourLogger
    {
        private const int MAX_GUESSES = 6;

        [Header("Words")]
        [SerializeField, ReadOnly] private string targetWord;
        [SerializeField, ReadOnly] private int currentGuess = 0;
        [SerializeField] private List<WordleWord> words;

        private readonly List<char> grayCharacters = new();
        private readonly List<char> yellowCharacters = new();
        private readonly List<char> greenCharacters = new();
        private WordleWord currentWord;

        public event Action Enabled;
        public event Action Disabled;
        public event Action<string> GrayCharacterAdded;
        public event Action<string> YellowCharacterAdded;
        public event Action<string> GreenCharacterAdded;

        public int CurrentWordLength => currentWord.Word.Length;

        private void Awake()
        {
            Invoke(nameof(Enable), 0.5f);
        }

        public void Enable()
        {
            currentGuess = 0;
            currentWord = words[0];
            targetWord = WordleDatabase.GetWord();

            CursorManager.SetCursorVisibility(true);
            CursorManager.SetCursorLockMode(CursorLockMode.None);

            Enabled?.Invoke();
        }

        public void Disable()
        {
            Disabled?.Invoke();
        }

        public void AddLetter(string letter)
        {
            currentWord.AddLetter(letter);
        }

        public void RemoveLetter()
        {
            currentWord.RemoveLetter();
        }

        public void SubmitGuess()
        {
            string guess = currentWord.Word;
            Log("Submit guess: " + guess);

            if (!WordleDatabase.IsValidWord(guess))
                return;

            if (guess == targetWord)
            {
                Disable();
                return;
            }

            for (int i = 0; i < guess.Length; i++)
            {
                char character = guess[i];
                
                if (!targetWord.Contains(character, StringComparison.OrdinalIgnoreCase))
                    currentWord.SetLetterType(i, WordleLetter.LetterType.Gray);
                else
                {
                    char targetCharacter = targetWord[i];

                    if (char.ToLower(targetCharacter).Equals(char.ToLower(character)))
                        currentWord.SetLetterType(i, WordleLetter.LetterType.Green);
                    else
                    {
                        int targetIndex = targetWord.IndexOf(character, StringComparison.OrdinalIgnoreCase);
                        bool needsLetter = false;

                        while (targetIndex != -1)
                        {
                            if (currentWord.GetLetter(targetIndex).Equals(targetCharacter.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                needsLetter = true;
                                break;
                            }

                            targetIndex = targetWord.IndexOf(character, targetIndex);
                        }

                        if (needsLetter)
                            currentWord.SetLetterType(i, WordleLetter.LetterType.Yellow);
                        else
                            currentWord.SetLetterType(i, WordleLetter.LetterType.Gray);
                    }
                }
            }

            foreach (var character in guess)
            {
                if (!targetWord.Contains(character, StringComparison.OrdinalIgnoreCase) && !grayCharacters.Contains(character))
                {
                    Log("Added gray character: " + character);

                    grayCharacters.Add(character);
                    GrayCharacterAdded?.Invoke(character.ToString());
                }
                else if (targetWord.Contains(character, StringComparison.OrdinalIgnoreCase))
                {
                    int currentIndex = guess.IndexOf(character);

                    if (!char.ToLower(targetWord[currentIndex]).Equals(char.ToLower(character)) && !greenCharacters.Contains(character) && !yellowCharacters.Contains(character))
                    {
                        Log("Added yellow character: " + character);

                        yellowCharacters.Add(character);
                        YellowCharacterAdded?.Invoke(character.ToString());
                    }
                    else if (char.ToLower(targetWord[currentIndex]).Equals(char.ToLower(character)) && !greenCharacters.Contains(character))
                    {
                        Log("Added green character: " + character);

                        greenCharacters.Add(character);
                        GreenCharacterAdded?.Invoke(character.ToString());
                    }
                    else
                        Log("Correct character already guessed: " + character, SHLogLevels.Verbose);
                }
                else
                    Log("Incorrect character already guessed: " + character, SHLogLevels.Verbose);
            }

            currentGuess++;
            if (currentGuess >= MAX_GUESSES)
                return;

            currentWord = words[currentGuess];
        }
    }
}
