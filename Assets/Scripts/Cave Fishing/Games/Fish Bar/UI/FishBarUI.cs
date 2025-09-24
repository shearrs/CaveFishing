using UnityEngine;

namespace CaveFishing.Games.FishBarGame.UI
{
    public class FishBarUI : MonoBehaviour
    {
        [SerializeField] private FishBar fishBar;
        [SerializeField] private Canvas gameCanvas;

        private void OnEnable()
        {
            fishBar.Enabled += OnEnabled;
            fishBar.Disabled += OnDisabled;
        }

        private void OnDisable()
        {
            fishBar.Enabled -= OnEnabled;
            fishBar.Disabled -= OnDisabled;
        }

        private void OnEnabled()
        {
            gameCanvas.enabled = true;
        }

        private void OnDisabled()
        {
            gameCanvas.enabled = false;
        }
    }
}
