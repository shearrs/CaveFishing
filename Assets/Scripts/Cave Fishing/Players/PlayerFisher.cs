using CaveFishing.Fishing;
using CaveFishing.Games;
using Shears.Input;
using Shears.Logging;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerFisher : SHMonoBehaviourLogger
    {
        [Header("Components")]
        [SerializeField] private FishingRod fishingRod;
        [SerializeField] private PlayerHolder holder;

        private bool wasEnabledBeforePause = false;
        private Fish currentFish;

        private void OnEnable()
        {
            fishingRod.FishReeled += OnFishReeled;

            SignalShuttle.Register<GameWonSignal>(OnGameWon);
            SignalShuttle.Register<GameLostSignal>(OnGameLost);
            SignalShuttle.Register<GamePausedChangedSignal>(OnGamePausedChanged);
        }

        private void OnDisable()
        {
            fishingRod.FishReeled -= OnFishReeled;

            SignalShuttle.Deregister<GameWonSignal>(OnGameWon);
            SignalShuttle.Deregister<GameLostSignal>(OnGameLost);
            SignalShuttle.Deregister<GamePausedChangedSignal>(OnGamePausedChanged);
        }

        private void Start()
        {
            Enable();
        }

        public void Enable()
        {
            fishingRod.Enable();
        }

        public void Disable()
        {
            fishingRod.Disable();
        }

        private void OnGamePausedChanged(GamePausedChangedSignal signal)
        {
            if (signal.IsPaused)
            {
                wasEnabledBeforePause = fishingRod.IsEnabled;
                Disable();
            }
            else
            {
                if (wasEnabledBeforePause)
                    Enable();
            }
        }

        private void OnFishReeled()
        {
            Log("Fish reeled.");
            currentFish = fishingRod.Bobber.GetFish();
            var minigame = MinigameManager.GetMinigame(currentFish.MinigameType);
            
            minigame.Enable();
        }

        private void OnGameWon(GameWonSignal signal)
        {
            Log("Game won!");

            if (currentFish == null)
                return;

            SignalShuttle.Emit(new FishCaughtSignal(currentFish));

            var fish = Instantiate(currentFish, fishingRod.Bobber.transform.position, Quaternion.identity);
            holder.Hold(fish);
        }

        private void OnGameLost(GameLostSignal signal)
        {
            Log("Game lost!");

            fishingRod.EnterState<ReturnState>();
        }
    }
}
