using Shears.Input;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame
{
    public class FishBar : Minigame
    {
        [Header("Input")]
        [SerializeField] private ManagedInputProvider inputProvider;

        [Header("Components")]
        [SerializeField] private SlidingBar slidingBar;
        [SerializeField] private SlidingFish slidingFish;

        private IManagedInput reelInput;

        private void Awake()
        {
            reelInput = inputProvider.GetInput("Reel");
        }

        private void Start()
        {
            Enable();
        }

        public override void Enable()
        {
            reelInput.Enable();
            slidingBar.Enable();
            slidingFish.Enable();

            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            reelInput.Disable();
            slidingBar.Disable();
            slidingFish.Disable();

            SignalShuttle.Emit(new GameDisabledSignal());
        }

        private void Update()
        {
            if (reelInput.IsPressed())
                slidingBar.MoveUp();
        }
    }
}
