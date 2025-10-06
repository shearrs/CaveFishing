using Mono.Cecil.Cil;
using Shears;
using Shears.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame
{
    public class FishTyping : Minigame
    {
        [Header("Components")]
        [SerializeField] private Instructor instructor;
        [SerializeField] private WordFish fishPrefab;
        [SerializeField] private Transform fishContainer;
        [SerializeField] private Keyboard keyboard;

        [Header("Game Settings")]
        [SerializeField, Min(10f)] private float gameTime;
        [SerializeField, Min(1f)] private float maxSpeedScaling;
        [SerializeField, Min(1f)] private float maxSpawnTimeScaling;

        [Header("Fish Settings")]
        [SerializeField] private Range<float> spawnTimeRange;
        [SerializeField] private float xSpawnPosition;
        [SerializeField] private Range<float> ySpawnRange;
        [SerializeField] private Range<float> fishSpeedRange = new(0.1f, 0.25f);

        private readonly Timer gameTimer = new();
        private readonly List<WordFish> spawnedFish = new();
        private bool isEnabled = false;

        public event Action Enabled;
        public event Action Disabled;

        private void Start()
        {
            Enable();
        }

        public override void Enable()
        {
            if (isEnabled)
                return;

            isEnabled = true;

            instructor.Instruct(StartGame);

            Enabled?.Invoke();
            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            if (!isEnabled)
                return;

            foreach (var fish in spawnedFish)
            {
                fish.ReachedEnd -= OnFishReachedEnd;
                Destroy(fish.gameObject);
            }

            keyboard.Disable();
            keyboard.InputLettersUpdated -= OnInputLettersUpdated;
            spawnedFish.Clear();
            StopAllCoroutines();

            isEnabled = false;
            Disabled?.Invoke();

            SignalShuttle.Emit(new GameDisabledSignal());
        }

        public void StartGame()
        {
            keyboard.Enable();
            keyboard.InputLettersUpdated += OnInputLettersUpdated;
            StartCoroutine(IESpawnFish());
        }

        private void OnInputLettersUpdated(string inputWord)
        {
            WordFish fishToType = null;

            foreach (var fish in spawnedFish)
            {
                if (fish.Word == inputWord)
                {
                    fishToType = fish;
                    break;
                }
            }

            if (fishToType != null)
            {
                spawnedFish.Remove(fishToType);
                fishToType.Type();
                keyboard.Clear();
            }
        }

        private IEnumerator IESpawnFish()
        {
            gameTimer.Start(gameTime);

            while (!gameTimer.IsDone)
            {
                float t = gameTimer.Percentage * gameTimer.Percentage;
                float spawnScale = Mathf.Lerp(1f, maxSpawnTimeScaling, t);
                float spawnTime = spawnTimeRange.Random() / spawnScale;

                yield return CoroutineUtil.WaitForSeconds(spawnTime);

                float speedScale = Mathf.Lerp(1f, maxSpeedScaling, t);
                var fish = Instantiate(fishPrefab, fishContainer);

                Vector2 position = new(xSpawnPosition, ySpawnRange.Random());
                fish.transform.localPosition = position;

                fish.Word = WordDatabase.GetWord();
                fish.Speed = fishSpeedRange.Random() * speedScale;

                fish.ReachedEnd += OnFishReachedEnd;
                spawnedFish.Add(fish);

                yield return null;
            }

            while (spawnedFish.Count > 0)
                yield return null;

            Disable();
            SignalShuttle.Emit(new GameWonSignal());
        }

        private void OnFishReachedEnd(WordFish fish)
        {
            Disable();
            SignalShuttle.Emit(new GameLostSignal());
        }
    }
}
