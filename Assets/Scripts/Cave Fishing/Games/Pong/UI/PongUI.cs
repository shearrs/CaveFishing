using UnityEngine;

namespace CaveFishing.Games.PongGame.UI
{
    public class PongUI : MonoBehaviour
    {
        [SerializeField] private Pong pong;
        [SerializeField] private Canvas canvas;

        private void OnEnable()
        {
            pong.Enabled += OnEnabled;
            pong.Disabled += OnDisabled;
        }

        private void OnDisable()
        {
            pong.Enabled -= OnEnabled;
            pong.Disabled -= OnDisabled;
        }

        private void OnEnabled()
        {
            canvas.enabled = true;
        }

        private void OnDisabled()
        {
            canvas.enabled = false;
        }
    }
}
