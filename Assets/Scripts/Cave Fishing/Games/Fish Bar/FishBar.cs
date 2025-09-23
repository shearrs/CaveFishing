using Shears;
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

        [Header("Game Settings")]
        [SerializeField, Min(0f)] private float reelPower = 1f;
        [SerializeField, Min(0f)] private float reelDecayAmount = 1f;
        [SerializeField, ReadOnly] private float currentReelAmount = 0f;

        private RectTransform barTransform;
        private RectTransform fishTransform;

        private IManagedInput reelInput;

        private void Awake()
        {
            reelInput = inputProvider.GetInput("Reel");

            barTransform = slidingBar.GetComponent<RectTransform>();
            fishTransform = slidingFish.GetComponent<RectTransform>();
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

            if (barTransform.GetWorldRect().Overlaps(fishTransform.GetWorldRect()))
                currentReelAmount += Time.deltaTime * reelPower;
            else
                currentReelAmount -= Time.deltaTime * reelDecayAmount;
        }
    }
}
