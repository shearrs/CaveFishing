using Shears.Input;
using Shears.Signals;
using System;
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
            progressBar.EmptyReelReached += OnEmptyReelReached;
        }

        private void OnDisable()
        {
            progressBar.FullReelReached -= OnFullReelReached;
            progressBar.EmptyReelReached -= OnEmptyReelReached;
        }

        public override void Enable()
        {
            slidingBar.SetProgress(0.45f);
            progressBar.SetReelAmount(0.5f);
            slidingFish.SetPosition(0.5f);

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

        public void StartGame()
        {
            reelInput.Enable();
            slidingBar.Enable();
            slidingFish.Enable();
            progressBar.Enable();
        }

        private void Update()
        {
            if (reelInput.IsPressed())
                slidingBar.MoveUp();
        }

        private void OnFullReelReached()
        {
            SignalShuttle.Emit(new GameWonSignal());
            Disable();
        }

        private void OnEmptyReelReached()
        {
            SignalShuttle.Emit(new GameLostSignal());
            Disable();
        }
    }
}
