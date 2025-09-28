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
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private FishingRod fishingRod;
        [SerializeField] private PlayerHolder holder;

        private IManagedInput castInput;
        private Fish currentFish;

        private void Awake()
        {
            castInput = inputProvider.GetInput("Cast");

            fishingRod.Disable();
        }

        private void OnEnable()
        {
            castInput.Started += OnCastInputStarted;
            castInput.Canceled += OnCastInputCanceled;
            fishingRod.FishReeled += OnFishReeled;

            SignalShuttle.Register<GameWonSignal>(OnGameWon);
            SignalShuttle.Register<GameLostSignal>(OnGameLost);
        }

        private void OnDisable()
        {
            castInput.Started -= OnCastInputStarted;
            castInput.Canceled -= OnCastInputCanceled;
            fishingRod.FishReeled -= OnFishReeled;

            SignalShuttle.Deregister<GameWonSignal>(OnGameWon);
            SignalShuttle.Deregister<GameLostSignal>(OnGameLost);
        }

        private void OnCastInputStarted(ManagedInputInfo info)
        {
            if (fishingRod.CurrentState == FishingRod.State.Disabled)
                return;
            else if (fishingRod.CurrentState == FishingRod.State.Casted)
                fishingRod.Reel();
            else
                fishingRod.BeginCharging();
        }

        private void OnCastInputCanceled(ManagedInputInfo info)
        {
            if (fishingRod.CurrentState == FishingRod.State.Disabled)
                return;
            else if (fishingRod.CurrentState == FishingRod.State.Charging)
                fishingRod.Cast();
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

            Instantiate(currentFish, fishingRod.Bobber.transform.position, Quaternion.identity);
            fishingRod.Disable();
            holder.Hold(currentFish);
        }

        private void OnGameLost(GameLostSignal signal)
        {
            Log("Game lost!");

            fishingRod.ReturnToRest();
        }
    }
}
