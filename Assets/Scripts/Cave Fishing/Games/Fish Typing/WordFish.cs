using Shears;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame
{
    public class WordFish : MonoBehaviour
    {
        [SerializeField, ReadOnly] private string word;
        [SerializeField, ReadOnly, Range(0f, 1f)] private float progress;
        [SerializeField] private float speed = 1.0f;

        public string Word { get => word; set => SetWord(word); }
        public float Speed { get => speed; set => speed = value; }

        public event Action<WordFish> ReachedEnd;
        public event Action<float> ProgressUpdated;
        public event Action<string> WordUpdated;

        private void Start()
        {
            StartCoroutine(IESwim());
        }

        public void Type()
        {
            Destroy(gameObject);
        }

        private void SetWord(string newWord)
        {
            word = newWord;
            WordUpdated?.Invoke(newWord);
        }

        private IEnumerator IESwim()
        {
            while (progress < 1.0f)
            {
                SetProgress(progress + (speed * Time.deltaTime));

                yield return null;
            }

            ReachedEnd?.Invoke(this);
        }

        private void SetProgress(float newProgress)
        {
            progress = Mathf.Clamp01(newProgress);

            ProgressUpdated?.Invoke(progress);
        }
    }
}
