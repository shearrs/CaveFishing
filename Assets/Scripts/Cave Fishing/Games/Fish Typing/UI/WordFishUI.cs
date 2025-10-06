using Shears;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame.UI
{
    public class WordFishUI : MonoBehaviour
    {
        [SerializeField] private WordFish fish;
        [SerializeField] private Range<float> movementRange;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            fish.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDisable()
        {
            fish.ProgressUpdated -= OnProgressUpdated;
        }

        private void OnProgressUpdated(float progress)
        {
            Vector2 position = rectTransform.localPosition;
            position.x = Mathf.Lerp(movementRange.Max, movementRange.Min, progress);

            rectTransform.localPosition = position;
        }
    }
}
