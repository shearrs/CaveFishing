using Shears.Input;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame
{
    public class FishBar : Minigame
    {
        [Header("Input")]
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private float movementSensitivity = 1.0f;

        private IManagedInput reelInput;

        private void Awake()
        {
            reelInput = inputProvider.GetInput("Reel");
        }

        public override void Enable()
        {
            reelInput.Enable();
            reelInput.Performed += OnReelInput;

            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            reelInput.Disable();
            reelInput.Performed -= OnReelInput;

            SignalShuttle.Emit(new GameDisabledSignal());
        }

        private void OnReelInput(ManagedInputInfo info)
        {
            // tell bar to move up
        }
    }
}
