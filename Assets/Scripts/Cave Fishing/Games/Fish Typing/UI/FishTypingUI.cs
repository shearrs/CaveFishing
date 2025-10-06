using UnityEngine;

namespace CaveFishing.Games.FishTypingGame.UI
{
    public class FishTypingUI : MonoBehaviour
    {
        [SerializeField] private FishTyping fishTyping;
        [SerializeField] private Canvas canvas;

        private void OnEnable()
        {
            fishTyping.Enabled += OnEnabled;
            fishTyping.Disabled += OnDisabled;
        }

        private void OnDisable()
        {
            fishTyping.Enabled -= OnEnabled;
            fishTyping.Disabled -= OnDisabled;
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
