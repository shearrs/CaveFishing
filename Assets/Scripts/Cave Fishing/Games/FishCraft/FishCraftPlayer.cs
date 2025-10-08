using CaveFishing.Players;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class FishCraftPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter character;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private PlayerCamera cam;
        [SerializeField] private BlockInteractor interactor;

        private bool isEnabled = false;

        private void OnEnable()
        {
            SignalShuttle.Register<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Register<InventoryClosedSignal>(OnInventoryClosed);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Deregister<InventoryClosedSignal>(OnInventoryClosed);
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            inventory.Enable();

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            inventory.Disable();

            isEnabled = false;
        }

        private void OnInventoryOpened(InventoryOpenedSignal signal)
        {
            character.Disable();
            cam.Disable();
            interactor.Disable();
        }

        private void OnInventoryClosed(InventoryClosedSignal signal)
        {
            character.Enable();
            cam.Enable();
            interactor.Enable();
        }
    }
}
