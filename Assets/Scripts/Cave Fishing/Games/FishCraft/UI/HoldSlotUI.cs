using Shears;
using Shears.Input;
using Shears.Signals;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame.UI
{
    public class HoldSlotUI : MonoBehaviour
    {
        [SerializeField] private RectTransform slotUI;

        private bool isMoving = false;

        private void OnEnable()
        {
            SignalShuttle.Register<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Register<InventoryClosedSignal>(OnInventoryClosed);
            SignalShuttle.Register<GameDisabledSignal>(OnGameDisabled);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<InventoryOpenedSignal>(OnInventoryOpened);
            SignalShuttle.Deregister<InventoryClosedSignal>(OnInventoryClosed);
            SignalShuttle.Deregister<GameDisabledSignal>(OnGameDisabled);
        }

        private void OnInventoryOpened(InventoryOpenedSignal signal)
        {
            if (isMoving)
                return;

            StartCoroutine(IEMove());
        }

        private void OnInventoryClosed(InventoryClosedSignal signal)
        {
            isMoving = false;
            StopAllCoroutines();
        }

        private void OnGameDisabled(GameDisabledSignal signal)
        {
            if (signal.Type == MinigameType.FishCraft)
            {
                isMoving = false;
                StopAllCoroutines();
            }
        }

        private IEnumerator IEMove()
        {
            isMoving = true;

            while (true)
            {
                Vector2 pointerPos = ManagedPointer.Current.Position;
                slotUI.position = pointerPos;

                yield return CoroutineUtil.WaitForEndOfFrame;
            }
        }
    }
}
