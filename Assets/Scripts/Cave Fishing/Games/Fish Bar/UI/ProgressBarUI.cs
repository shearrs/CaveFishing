using Shears;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private RectTransform fillTransform;
        [SerializeField] private Range<float> fillRange = new(5f, 590f);

        private void OnEnable()
        {
            progressBar.ReelAmountUpdated += OnReelAmountUpdated;
        }

        private void OnDisable()
        {
            progressBar.ReelAmountUpdated -= OnReelAmountUpdated;
        }

        private void OnReelAmountUpdated(float progress)
        {
            var offset = fillTransform.offsetMax;

            offset.y = -Mathf.Lerp(fillRange.Max, fillRange.Min, progress);

            fillTransform.offsetMax = offset;
        }
    }
}
