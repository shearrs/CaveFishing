using CaveFishing.Games;
using CaveFishing.Items;
using UnityEngine;

namespace CaveFishing.Fishing
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fish : MonoBehaviour, IItem
    {
        [SerializeField] private MinigameType minigameType;
        private Rigidbody rb;
        private Collider[] colliders;

        public MinigameType MinigameType => minigameType;

        private void Awake()
        {
            rb  = GetComponent<Rigidbody>();

            colliders = GetComponentsInChildren<Collider>();
        }

        public void Hold()
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;

            foreach (var collider in colliders)
                collider.enabled = false;
        }

        public void Release()
        {
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            foreach (var collider in colliders)
                collider.enabled = true;
        }
    }
}
