using Shears;
using System;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RectTransform barTransform;
        [SerializeField] private RectTransform fishTransform;

        [Header("Settings")]
        [SerializeField, Min(0f)] private float reelPower = .25f;
        [SerializeField, Min(0f)] private float reelDecayAmount = .25f;
        [SerializeField, ReadOnly, Range(0f, 1f)] private float currentReelAmount = 0.5f;

        private bool isEnabled = false;

        public event Action<float> ReelAmountUpdated;

        public void Enable()
        {
            currentReelAmount = 0.5f;
            isEnabled = true;
        }

        public void Disable()
        {
            isEnabled = false;
        }

        private void Update()
        {
            if (!isEnabled)
                return;

            if (barTransform.GetWorldRect().Overlaps(fishTransform.GetWorldRect()))
                currentReelAmount += Time.deltaTime * reelPower;
            else
                currentReelAmount -= Time.deltaTime * reelDecayAmount;

            currentReelAmount = Mathf.Clamp01(currentReelAmount);

            ReelAmountUpdated?.Invoke(currentReelAmount);
        }
    }
}
