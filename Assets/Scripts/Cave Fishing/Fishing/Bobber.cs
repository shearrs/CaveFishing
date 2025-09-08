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

        public void SetPosition(Vector3 position)
        {
            rb.position = position;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("bobber trigger");
        }
    }
}
