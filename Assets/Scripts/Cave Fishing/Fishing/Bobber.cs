using CaveFishing.Games;
using Shears.Tweens;
using System;
using UnityEngine;

namespace CaveFishing.Fishing
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Bobber : MonoBehaviour
    {
        [SerializeField] private float biteOffset = 0.15f;

        private Rigidbody rb;
        private FishingSpot currentSpot;
        private bool isBiting = false;

        public bool IsBiting => isBiting;

        public event Action EnteredWater;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Cast(Vector3 origin, Vector3 force)
        {
            rb.position = origin;
            rb.rotation = Quaternion.identity;
            rb.PublishTransform();

            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            
            rb.AddForce(force, ForceMode.Impulse);
        }

        public void BeginBite()
        {
            Vector3 offset = biteOffset * Vector3.down;

            rb.DoMoveTween(rb.position + offset, new StructTweenData(0.5f, easingFunction: EasingFunction.Ease.EaseOutBack));

            isBiting = true;
        }

        public Minigame GetMinigame()
        {
            return currentSpot.GetMinigame();
        }

        public void EndBite()
        {
            Vector3 offset = biteOffset * Vector3.up;

            rb.DoMoveTween(rb.position + offset, new StructTweenData(0.5f, easingFunction: EasingFunction.Ease.EaseOutBack));

            isBiting = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out FishingSpot fishingSpot))
                return;

            rb.isKinematic = true;

            currentSpot = fishingSpot;
            EnteredWater?.Invoke();
        }
    }
}
