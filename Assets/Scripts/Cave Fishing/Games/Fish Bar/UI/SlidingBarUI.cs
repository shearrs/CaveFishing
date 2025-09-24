using UnityEngine;

namespace CaveFishing.Games.FishBarGame.UI
{
    public class SlidingBarUI : MonoBehaviour
    {
        [SerializeField] private SlidingBar slidingBar;
        [SerializeField] private Vector2 topPosition;
        [SerializeField] private Vector2 bottomPosition;

        private void OnEnable()
        {
            slidingBar.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDisable()
        {
            slidingBar.ProgressUpdated -= OnProgressUpdated;
        }

        private void OnProgressUpdated(float progress)
        {
            slidingBar.transform.localPosition = Vector3.Lerp(bottomPosition, topPosition, progress);
        }
    }
}
