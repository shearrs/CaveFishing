using CaveFishing.Fishing;
using Shears;
using Shears.Input;
using Shears.Logging;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerFisher : SHMonoBehaviourLogger
    {
        [Header("Components")]
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private FishingRod fishingRod;

        private IManagedInput castInput;

        private void Awake()
        {
            castInput = inputProvider.GetInput("Cast");
        }

        private void OnEnable()
        {
            castInput.Started += OnCastInputStarted;
            castInput.Canceled += OnCastInputCanceled;
            fishingRod.FishReeled += OnFishReeled;
        }

        private void OnDisable()
        {
            castInput.Started -= OnCastInputStarted;
            castInput.Canceled -= OnCastInputCanceled;
            fishingRod.FishReeled -= OnFishReeled;
        }

        private void OnCastInputStarted(ManagedInputInfo info)
        {
            if (fishingRod.CurrentState == FishingRod.State.Casted)
                fishingRod.Reel();
            else
                fishingRod.BeginCharging();
        }

        private void OnCastInputCanceled(ManagedInputInfo info)
        {
            if (fishingRod.CurrentState == FishingRod.State.Charging)
                fishingRod.Cast();
        }

        private void OnFishReeled()
        {
            Log("Fish reeled.");
            var minigame = fishingRod.Bobber.GetMinigame();

            minigame.Enable();
        }
    }
}
