using Shears.Detection;
using Shears.Input;
using Shears.Interaction;
using Shears.Logging;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class BlockInteractor : SHMonoBehaviourLogger
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private AreaDetector3D detector;

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
                    bool isInputPressed = interactInput.IsPressed();

                    if (!targetedBlock.IsBreaking && isInputPressed)
                        targetedBlock.BeginBreaking();
                    else if (targetedBlock.IsBreaking && !isInputPressed)
                        targetedBlock.EndBreaking();
                }

                yield return null;
            }
        }

        private void UpdateTargetedBlock()
        {
            previousBlock = targetedBlock;

            detector.Detect();
            detector.TryGetDetection(out targetedBlock, true);

            if (targetedBlock != null)
                targetedBlock.BeginHover();
        }
    }
}
