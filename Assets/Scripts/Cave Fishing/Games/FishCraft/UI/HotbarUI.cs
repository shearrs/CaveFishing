using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class HotbarUI : MonoBehaviour
    {
        [SerializeField] private GameObject container;

        private void OnEnable()
        {
            SignalShuttle.Register<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Register<InventoryClosedSignal>(OnInventoryClosed);
            SignalShuttle.Register<GameEnabledSignal>(OnGameEnabled);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Deregister<InventoryClosedSignal>(OnInventoryClosed);
            SignalShuttle.Deregister<GameEnabledSignal>(OnGameEnabled);
        }

        private void OnInventoryOpened(InventoryOpenedSignal signal)
        {
            container.SetActive(false);
        }

        private void OnInventoryClosed(InventoryClosedSignal signal)
        {
            container.SetActive(true);
        }

        private void OnGameEnabled(GameEnabledSignal signal)
        {
            if (signal.Type == MinigameType.FishCraft)
                container.SetActive(true);
        }
    }
}
