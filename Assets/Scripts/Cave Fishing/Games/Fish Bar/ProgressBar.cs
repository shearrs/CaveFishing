using Shears;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RectTransform barTransform;
        [SerializeField] private RectTransform fishTransform;

        [Header("Settings")]
        [SerializeField, Min(0f)] private float reelPower = .18f;
        [SerializeField, Min(0f)] private float reelDecayAmount = .35f;
        [SerializeField, Min(0f)] private float lenienceDecayAmount = .2f;
        [SerializeField, ReadOnly, Range(0f, 1f)] private float currentReelAmount = 0.5f;

        private readonly Timer lenienceTimer = new(0.85f);

        public event Action<float> ReelAmountUpdated;
        public event Action FullReelReached;

        public void Enable()
        {
            currentReelAmount = 0.5f;

            lenienceTimer.Start();
            StartCoroutine(IEUpdate());
        }

        public void Disable()
        {
            lenienceTimer.Stop();
            StopAllCoroutines();
        }

        private IEnumerator IEUpdate()
        {
            while(true)
            {
                UpdateReelAmount();

                yield return null;
            }
        }

        private void UpdateReelAmount()
        {
            float decayAmount = lenienceTimer.IsDone ? reelDecayAmount : lenienceDecayAmount;

            if (barTransform.GetWorldRect().Overlaps(fishTransform.GetWorldRect()))
                currentReelAmount += Time.deltaTime * reelPower;
            else
                currentReelAmount -= Time.deltaTime * decayAmount;

            currentReelAmount = Mathf.Clamp01(currentReelAmount);

            ReelAmountUpdated?.Invoke(currentReelAmount);

            if (currentReelAmount == 1.0f)
                FullReelReached?.Invoke();
        }
    }
}
