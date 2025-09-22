using Shears;
using Shears.Tweens;
using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

namespace CaveFishing.Games.FishBarGame
{
    public class SlidingFish : MonoBehaviour
    {
        [SerializeField, ReadOnly, Range(0f, 1f)] private float progress = 0.0f;
        [SerializeField] private StructTweenData tweenData;

        [Header("Min Movement Range")]
        [SerializeField] private float minMinMoveTime = 0.25f;
        [SerializeField] private float maxMinMoveTime = 1f;

        [Header("Max Movement Range")]
        [SerializeField] private float minMaxMoveTime = 2f;
        [SerializeField] private float maxMaxMoveTime = 3f;

        public event Action<float> ProgressUpdated;

        public void Enable()
        {
            StartCoroutine(IEMove());

            progress = 0.5f;
            ProgressUpdated?.Invoke(progress);
        }

        public void Disable()
        {
            StopAllCoroutines();
        }

        private IEnumerator IEMove()
        {
            float minMoveTime = Random.Range(minMinMoveTime, maxMinMoveTime);
            float maxMoveTime = Random.Range(minMaxMoveTime, maxMaxMoveTime);

            while (true)
            {
                float startProgress = progress;
                float newProgress = Random.Range(0f, 1f);
                var tween = TweenManager.DoTween((t) => progress = Mathf.LerpUnclamped(startProgress, newProgress, t), tweenData);

                while (tween.IsPlaying)
                {
                    ProgressUpdated?.Invoke(progress);

                    yield return null;
                }

                ProgressUpdated?.Invoke(newProgress);

                yield return CoroutineUtil.WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));
            }
        }
    }
}
