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

        public string Word { get => word; set => word = value; }
        public float Speed { get => speed; set => speed = value; }

        public event Action<WordFish> ReachedEnd;
        public event Action<float> ProgressUpdated;

        private void Start()
        {
            StartCoroutine(IESwim());
        }

        public void Type()
        {
            Destroy(gameObject);
        }

        private IEnumerator IESwim()
        {
            while (true)
            {
                SetProgress(progress + (speed * Time.deltaTime));

                if (progress == 1.0f)
                    break;

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
