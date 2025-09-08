using UnityEngine;

namespace CaveFishing.Fishing
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Bobber : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("bobber trigger");
        }
    }
}
