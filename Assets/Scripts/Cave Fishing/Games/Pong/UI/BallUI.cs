using Shears;
using UnityEngine;

namespace CaveFishing.Games.PongGame.UI
{
    public class BallUI : MonoBehaviour
    {
        [SerializeField] private Ball ball;
        [SerializeField] private Range<float> xPositionRange;
        [SerializeField] private Range<float> yPositionRange;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            ball.ProgressUpdated += OnProgressUpdated;
        }

        private void OnDisable()
        {
            ball.ProgressUpdated -= OnProgressUpdated;
        }   
        
        private void OnProgressUpdated(Vector2 progress)
        {
            Vector2 position;

            position.x = xPositionRange.Lerp(progress.x);
            position.y = yPositionRange.Lerp(progress.y);

            rectTransform.localPosition = position;
        }
    }
}
