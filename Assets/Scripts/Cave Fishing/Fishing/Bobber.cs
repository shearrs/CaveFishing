using UnityEngine;

namespace CaveFishing.Fishing
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Bobber : MonoBehaviour
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Cast(Vector3 origin, Vector3 force)
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.position = origin;
            rb.rotation = Quaternion.identity;
            rb.linearVelocity = Vector3.zero;

            rb.AddForce(force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out FishingSpot fishingSpot))
                return;

            rb.isKinematic = true;
        }
    }
}
