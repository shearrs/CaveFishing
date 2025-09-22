using UnityEngine;

namespace CaveFishing.Games.FishBarGame.UI
{
    public class SlidingFishUI : MonoBehaviour
    {
        [SerializeField] private SlidingFish slidingFish;
        [SerializeField] private Vector2 topPosition;
        [SerializeField] private Vector2 bottomPosition;

        private void OnEnable()
        {
            slidingFish.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDisable()
        {
            slidingFish.ProgressUpdated -= OnProgressUpdated;
        }

        private void OnProgressUpdated(float progress)
        {
            transform.localPosition = Vector3.Lerp(bottomPosition, topPosition, progress);
        }
    }
}
