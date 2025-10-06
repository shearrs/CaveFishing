using System;
using Shears;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace CaveFishing.Games.FishTypingGame
{
    public class FishTyping : Minigame
    {
        [Header("Components")]
        [SerializeField] private WordFish fishPrefab;
        [SerializeField] private Transform fishContainer;

        [Header("Settings")]
        [SerializeField, Min(10f)] private float gameTime;
        [SerializeField] private Range<float> spawnTimeRange;
        [SerializeField] private float xSpawnPosition;
        [SerializeField] private Range<float> ySpawnRange;

        private readonly List<WordFish> spawnedFish = new();
        private bool isEnabled = false;

        public event Action Enabled;
        public event Action Disabled;

        public override void Enable()
        {
            if (isEnabled)
                return;

            StartCoroutine(IESpawnFish());

            isEnabled = true;
            Enabled?.Invoke();
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

            spawnedFish.Clear();
            StopAllCoroutines();

            isEnabled = false;
            Disabled?.Invoke();
        }

        private IEnumerator IESpawnFish()
        {
            float spawnTime = spawnTimeRange.Random();

            while (true)
            {
                yield return CoroutineUtil.WaitForSeconds(spawnTime);

                var fish = Instantiate(fishPrefab, fishContainer);

                Vector2 position = new(xSpawnPosition, ySpawnRange.Random());
                fish.transform.localPosition = position;

                fish.ReachedEnd += OnFishReachedEnd;

                yield return null;
            }
        }

        private void OnFishReachedEnd(WordFish fish)
        {
            
        }
    }
}
