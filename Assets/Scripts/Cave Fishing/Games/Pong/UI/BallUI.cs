using Shears;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.PongGame.UI
{
    public class BallUI : MonoBehaviour
    {
        [SerializeField] private Ball ball;
        [SerializeField] private Image image;
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
            ball.Enabled += OnEnabled;
            ball.Disabled += OnDisabled;
        }

        private void OnDisable()
        {
            ball.ProgressUpdated -= OnProgressUpdated;
            ball.Enabled -= OnEnabled;
            ball.Disabled -= OnDisabled;
        }   
        
        private void OnEnabled()
        {
            image.enabled = true;
        }

        private void OnDisabled()
        {
            image.enabled = false;
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
