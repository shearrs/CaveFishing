using Shears.Input;
using Shears.Signals;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CaveFishing.Games.FishBarGame
{
    public class FishBar : Minigame
    {
        [Header("Input")]
        [SerializeField] private ManagedInputProvider inputProvider;

        [Header("Components")]
        [SerializeField] private SlidingBar slidingBar;
        [SerializeField] private SlidingFish slidingFish;
        [SerializeField] private ProgressBar progressBar;

        private IManagedInput reelInput;

        public event Action Enabled;
        public event Action Disabled;

        private void Awake()
        {
            reelInput = inputProvider.GetInput("Reel");
        }

        private void OnEnable()
        {
            progressBar.FullReelReached += OnFullReelReached;
        }

        private void OnDisable()
        {
            progressBar.FullReelReached -= OnFullReelReached;
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
            progressBar.Enable();

            Enabled?.Invoke();
            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            reelInput.Disable();
            slidingBar.Disable();
            slidingFish.Disable();
            progressBar.Disable();

            Disabled?.Invoke();
            SignalShuttle.Emit(new GameDisabledSignal());
        }

        private void Update()
        {
            if (reelInput.IsPressed())
                slidingBar.MoveUp();
        }

        private void OnFullReelReached()
        {
            Disable();
        }
    }
}
