using Shears;
using UnityEngine;

namespace CaveFishing.Games.PongGame.UI
{
    public class PaddleUI : MonoBehaviour
    {
        [SerializeField] private Paddle paddle;
        [SerializeField] private Range<float> movementRange;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            paddle.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDisable()
        {
            paddle.ProgressUpdated -= OnProgressUpdated;
        }

        private void OnProgressUpdated(float progress)
        {
            var position = rectTransform.localPosition;
            position.y = movementRange.Lerp(progress);

            rectTransform.localPosition = position;
        }
    }
}
