using CaveFishing.Games.UI;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame.UI
{
    public class FishTypingUI : MonoBehaviour
    {
        [SerializeField] private FishTyping fishTyping;
        [SerializeField] private Canvas canvas;
        [SerializeField] private InstructionText instructionText;

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
            instructionText.Display(fishTyping.StartGame);
        }

        private void OnDisabled()
        {
            canvas.enabled = false;
        }
    }
}
