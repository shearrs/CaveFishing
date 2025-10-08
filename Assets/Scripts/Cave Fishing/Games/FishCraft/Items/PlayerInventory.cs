using Shears.Input;
using System;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private ManagedInputProvider inputProvider;

        private IManagedInput toggleInput;
        private bool isEnabled = false;

        private void Awake()
        {
            toggleInput = inputProvider.GetInput("Toggle Inventory");
        }

        private void OnDisable()
        {
            Disable();
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            toggleInput.Performed += OnToggleInput;

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            toggleInput.Performed -= OnToggleInput;

            isEnabled = false;
        }

        private void OnToggleInput(ManagedInputInfo info)
        {
            inventory.Toggle();
        }
    }
}
