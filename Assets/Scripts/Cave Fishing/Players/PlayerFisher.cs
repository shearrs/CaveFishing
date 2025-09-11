using CaveFishing.Fishing;
using Shears.Input;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerFisher : MonoBehaviour
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private Transform relativeTransform;
        [SerializeField] private FishingRod fishingRod;

        private IManagedInput castInput;

        private void Awake()
        {
            castInput = inputProvider.GetInput("Cast");
        }

        private void Start()
        {
            fishingRod.transform.SetParent(relativeTransform);
        }

        private void OnEnable()
        {
            castInput.Started += OnCastInputStarted;
            castInput.Canceled += OnCastInputCanceled;
        }

        private void OnDisable()
        {
            castInput.Started -= OnCastInputStarted;
            castInput.Canceled -= OnCastInputCanceled;
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
    }
}
