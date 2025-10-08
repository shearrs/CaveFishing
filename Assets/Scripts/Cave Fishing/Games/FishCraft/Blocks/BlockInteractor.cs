using Shears.Detection;
using Shears.Input;
using Shears.Logging;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class BlockInteractor : SHMonoBehaviourLogger
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private RayDetector3D detector;
        [SerializeField] private ItemHolder holder;
        [SerializeField] private Inventory inventory;

        private IManagedInput interactInput;
        private IManagedInput alternativeInput;
        private Block previousBlock;
        private Block targetedBlock;
        private bool isEnabled = false;

        private void Awake()
        {
            interactInput = inputProvider.GetInput("Interact");
            alternativeInput = inputProvider.GetInput("Alternative Interact");
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            StartCoroutine(IEPollInput());

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            StopAllCoroutines();

            isEnabled = false;
        }

        private IEnumerator IEPollInput()
        {
            while (true)
            {
                UpdateTargetedBlock();

                if (previousBlock != targetedBlock)
                {
                    if (previousBlock != null)
                    {
                        previousBlock.EndHover();
                        previousBlock.EndBreaking();
                    }
                }

                if (targetedBlock != null)
                {
                    if (!targetedBlock.IsBreaking && interactInput.IsPressed())
                        targetedBlock.BeginBreaking();
                    else if (targetedBlock.IsBreaking && !interactInput.IsPressed())
                        targetedBlock.EndBreaking();

                    if (alternativeInput.WasPressedThisFrame() && holder.HeldItem != null)
                        PlaceBlock();
                }

                yield return null;
            }
        }

        private void PlaceBlock()
        {
            var block = Instantiate(holder.HeldItem.Data.Block);
            var hit = detector.GetHit(0);

            block.transform.SetParent(targetedBlock.transform.parent);
            block.transform.position = targetedBlock.transform.position + hit.normal;

            holder.HeldSlot.RemoveCount(1);
        }

        private void UpdateTargetedBlock()
        {
            previousBlock = targetedBlock;

            detector.Detect();
            detector.TryGetDetection(out Block newTarget, true);

            if (newTarget == targetedBlock)
                return;

            if (targetedBlock != null)
                targetedBlock.Broke -= OnTargetBroke;

            targetedBlock = newTarget;

            if (targetedBlock != null)
            {
                targetedBlock.BeginHover();
                targetedBlock.Broke += OnTargetBroke;
            }
        }

        private void OnTargetBroke()
        {
            if (targetedBlock.Drop == null)
                return;

            var item = new Item(targetedBlock.Drop);
            inventory.AddItem(item);
        }
    }
}
