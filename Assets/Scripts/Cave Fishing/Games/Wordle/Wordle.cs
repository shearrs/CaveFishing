using Shears;
using Shears.Input;
using Shears.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.WordleGame
{
    public class Wordle : Minigame
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
        private readonly List<WordleLetter> guessLetters = new();
        private readonly List<char> targetCharacters = new();
        private bool isEnding = false;
        private WordleWord currentWord;

        public WordleWord CurrentWord => currentWord;
        public int CurrentWordLength => currentWord.Word.Length;

        public event Action Enabled;
        public event Action Disabled;
        public event Action InvalidWordSubmitted;

        public override void Enable()
        {
            isEnding = false;

            currentGuess = 0;
            currentWord = words[0];
            targetWord = WordleDatabase.GetWord();

            ResetLetters();

            CursorManager.SetCursorVisibility(true);
            CursorManager.SetCursorLockMode(CursorLockMode.None);

            Enabled?.Invoke();
            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            isEnding = false;

            Disabled?.Invoke();
            SignalShuttle.Emit(new GameDisabledSignal());
        }

        private void ResetLetters()
        {
            grayCharacters.Clear();
            yellowCharacters.Clear();
            greenCharacters.Clear();

            foreach (var word in words)
            {
                foreach (var letter in word.WordleLetters)
                    letter.SetType(WordleLetter.LetterType.None);
            }

            foreach (var letter in keyboard)
                letter.SetType(WordleLetter.LetterType.None);
        }

        public void AddLetter(string letter)
        {
            if (isEnding)
                return;

            currentWord.AddLetter(letter);
        }

        public void RemoveLetter()
        {
            if (isEnding)
                return;

            currentWord.RemoveLetter();
        }

        public void SubmitGuess()
        {
            if (isEnding)
                return;

            string guess = currentWord.Word;
            Log("Submit guess: " + guess);

            if (!WordleDatabase.IsValidWord(guess))
            {
                InvalidWordSubmitted?.Invoke();
                return;
            }

            guessLetters.Clear();
            targetCharacters.Clear();
            guessLetters.AddRange(currentWord.WordleLetters);
            targetCharacters.AddRange(targetWord);

            for (int i = 0; i < guess.Length; i++)
            {
                char guessCharacter = guess[i];

                if (!targetCharacters.Contains(guessCharacter))
                {
                    currentWord.SetLetterType(i, WordleLetter.LetterType.Gray);

                    if (!grayCharacters.Contains(guessCharacter))
                    {
                        grayCharacters.Add(guessCharacter);
                        SetKeyboardLetter(guessCharacter.ToString(), WordleLetter.LetterType.Gray);
                    }

                    guessLetters.Remove(currentWord.WordleLetters[i]);
                }
                else if (guessCharacter == targetWord[i])
                {
                    currentWord.SetLetterType(i, WordleLetter.LetterType.Green);

                    if (!greenCharacters.Contains(guessCharacter))
                    {
                        yellowCharacters.Remove(guessCharacter);
                        greenCharacters.Add(guessCharacter);

                        SetKeyboardLetter(guessCharacter.ToString(), WordleLetter.LetterType.Green);
                    }

                    guessLetters.Remove(currentWord.WordleLetters[i]);
                    targetCharacters.Remove(guessCharacter);
                }
            }

            // these are all possible yellows
            for (int i = 0; i < guessLetters.Count; i++)
            {
                var letter = guessLetters[i];
                char character = letter.Letter[0];

                if (!targetCharacters.Contains(character))
                {
                    letter.SetType(WordleLetter.LetterType.Gray, currentWord.IndexOf(letter));

                    continue;
                }

                letter.SetType(WordleLetter.LetterType.Yellow, currentWord.IndexOf(letter));

                if (!greenCharacters.Contains(character) && !yellowCharacters.Contains(character))
                {
                    yellowCharacters.Add(character);
                    SetKeyboardLetter(character.ToString(), WordleLetter.LetterType.Yellow);
                }

                targetCharacters.Remove(character);
            }

            if (guess == targetWord)
            {
                StartCoroutine(IEEndGame(true));
                return;
            }

            currentGuess++;
            if (currentGuess >= MAX_GUESSES)
            {
                StartCoroutine(IEEndGame(false));
                return;
            }

            currentWord = words[currentGuess];
        }
    
        private void SetKeyboardLetter(string letter, WordleLetter.LetterType type)
        {
            foreach (var wordleLetter in keyboard)
            {
                if (wordleLetter.Letter == letter)
                {
                    wordleLetter.SetType(type);
                    break;
                }
            }
        }
    
        private IEnumerator IEEndGame(bool win)
        {
            isEnding = true;

            yield return CoroutineUtil.WaitForSeconds(3.0f);

            Disable();

            if (win)
                SignalShuttle.Emit(new GameWonSignal());
            else
                SignalShuttle.Emit(new GameLostSignal());
        }
    }
}
