using TMPro;
using UnityEngine;

namespace CaveFishing.Games.PongGame.UI
{
    public class PongUI : MonoBehaviour
    {
        [SerializeField] private Pong pong;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            pong.Enabled += OnEnabled;
            pong.Disabled += OnDisabled;
            pong.ScoreUpdated += OnScoreUpdated;
        }

        private void OnDisable()
        {
            pong.Enabled -= OnEnabled;
            pong.Disabled -= OnDisabled;
            pong.ScoreUpdated -= OnScoreUpdated;
        }

        private void OnEnabled()
        {
            canvas.enabled = true;

            textMesh.text = $"0 : 0";
        }

        private void OnDisabled()
        {
            canvas.enabled = false;
        }

        private void OnScoreUpdated()
        {
            textMesh.text = $"{pong.PlayerScore} : {pong.BotScore}";
        }
    }
}
