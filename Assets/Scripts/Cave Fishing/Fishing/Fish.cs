using CaveFishing.Games;
using CaveFishing.Items;
using UnityEngine;

namespace CaveFishing.Fishing
{
    [RequireComponent(typeof(Rigidbody)), SelectionBase]
    public class Fish : MonoBehaviour, IItem
    {
        [SerializeField] private MinigameType minigameType;
        [SerializeField] private Vector3 holdRotation = new(0f, 90f, 0f);
        private Rigidbody rb;
        private Collider[] colliders;

        public MinigameType MinigameType => minigameType;
        public Quaternion HoldRotation => Quaternion.Euler(holdRotation);

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            colliders = GetComponentsInChildren<Collider>();
        }

        public void Hold()
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.None;

            foreach (var collider in colliders)
                collider.enabled = false;
        }

        public void Release(ReleaseData data)
        {
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            foreach (var collider in colliders)
                collider.enabled = true;

            rb.AddForce(data.ReleaseVelocity, ForceMode.Impulse);
        }
    }
}
