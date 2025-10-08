using Shears;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    [SelectionBase]
    public class Block : MonoBehaviour
    {
        [SerializeField] private bool isBreakable = true;
        [SerializeField, Min(0.1f), ShowIf("isBreakable")] private float timeToBreak = 1f;
        [SerializeField, ShowIf("isBreakable")] private ItemData drop;

        private bool isBreaking = false;

        public bool IsBreaking => isBreaking;
        public ItemData Drop => drop;

        public event Action HoverBegan;
        public event Action HoverEnded;
        public event Action<float> BreakAmountUpdated;
        public event Action Broke;

        public void ResetBlock()
        {
            gameObject.SetActive(true);

            EndBreaking();
            EndHover();
        }

        public void BeginBreaking()
        {
            if (isBreaking || !isBreakable)
                return;

            Debug.Log("Begin breaking: " + name);

            StartCoroutine(IEBreak());
        }

        public void EndBreaking()
        {
            if (!isBreaking) 
                return;

            Debug.Log("End breaking: " + name);

            StopAllCoroutines();
            BreakAmountUpdated?.Invoke(0f);
            isBreaking = false;
        }

        public void Break()
        {
            if (!isBreakable)
                return;

            Broke?.Invoke();
            gameObject.SetActive(false);
        }

        public void BeginHover()
        {
            HoverBegan?.Invoke();
        }

        public void EndHover()
        {
            HoverEnded?.Invoke();
        }

        private IEnumerator IEBreak()
        {
            float elapsedTime = 0;
            isBreaking = true;

            while (elapsedTime < timeToBreak)
            {
                float t = elapsedTime / timeToBreak;

                BreakAmountUpdated?.Invoke(t);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            isBreaking = false;
            Break();
        }
    }
}
