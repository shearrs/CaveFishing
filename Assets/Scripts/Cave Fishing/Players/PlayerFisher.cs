using CaveFishing.Fishing;
using Shears.Input;
using UnityEngine;

namespace CaveFishing.Players
{
    public class PlayerFisher : MonoBehaviour
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private FishingRod fishingRod;

        private IManagedInput castInput;

        private void Awake()
        {
            castInput = inputProvider.GetInput("Cast");
        }

        private void OnEnable()
        {
            castInput.Performed += OnCastInput;
        }

        private void OnDisable()
        {
            castInput.Performed -= OnCastInput;
        }

        private void OnCastInput(ManagedInputInfo info)
        {
            fishingRod.Cast();
        }
    }
}
