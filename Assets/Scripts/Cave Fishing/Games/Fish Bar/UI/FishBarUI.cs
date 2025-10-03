using CaveFishing.Games.UI;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame.UI
{
    public class FishBarUI : MonoBehaviour
    {
        [SerializeField] private FishBar fishBar;
        [SerializeField] private Canvas gameCanvas;
        [SerializeField] private InstructionText instructionText;

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
            instructionText.Display(fishBar.StartGame);
        }

        private void OnDisabled()
        {
            gameCanvas.enabled = false;
        }
    }
}
