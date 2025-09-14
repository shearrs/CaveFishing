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
        [SerializeField] private List<WordleLetter> keyboard;

        private readonly List<char> grayCharacters = new();
        private readonly List<char> yellowCharacters = new();
        private readonly List<char> greenCharacters = new();
        private WordleWord currentWord;

        public event Action Enabled;
        public event Action Disabled;
        public event Action InvalidWordSubmitted;

        public WordleWord CurrentWord => currentWord;
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
            {
                InvalidWordSubmitted?.Invoke();
                return;
            }

            if (guess == targetWord)
            {
                Disable();
                return;
            }

            for (int i = 0; i < guess.Length; i++)
            {
                char character = guess[i];
                
                if (!targetWord.Contains(character))
                {
                    currentWord.SetLetterType(i, WordleLetter.LetterType.Gray);

                    if (!grayCharacters.Contains(character))
                        grayCharacters.Add(character);
                }
                else
                {
                    char targetCharacter = targetWord[i];

                    if (targetCharacter == character)
                    {
                        currentWord.SetLetterType(i, WordleLetter.LetterType.Green);

                        if (!greenCharacters.Contains(targetCharacter))
                            greenCharacters.Add(targetCharacter);
                    }
                    else
                    {
                        int targetIndex = targetWord.IndexOf(character);
                        bool needsLetter = false;

                        while (targetIndex != -1)
                        {
                            if (currentWord.GetLetter(targetIndex) != targetCharacter.ToString())
                            {
                                needsLetter = true;
                                break;
                            }

                            if (targetIndex == targetWord.Length - 1)
                                break;

                            targetIndex = targetWord.IndexOf(character, targetIndex + 1);

                            Log($"Index: {targetIndex}", SHLogLevels.Fatal);
                        }

                        if (needsLetter)
                        {
                            currentWord.SetLetterType(i, WordleLetter.LetterType.Yellow);

                            if (!yellowCharacters.Contains(targetCharacter))
                                yellowCharacters.Add(targetCharacter);
                        }
                        else
                        {
                            currentWord.SetLetterType(i, WordleLetter.LetterType.Gray);

                            if (!grayCharacters.Contains(targetCharacter))
                                grayCharacters.Add(targetCharacter);
                        }
                    }
                }
            }

            currentGuess++;
            if (currentGuess >= MAX_GUESSES)
                return;

            currentWord = words[currentGuess];
        }
    }
}
