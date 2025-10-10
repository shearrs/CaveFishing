using System;
using UnityEngine;

namespace CaveFishing.Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private Vector3 holdRotation = new(0f, 90f, 0f);

        public event Action Held;
        public event Action<ReleaseData> Released;

        public Quaternion HoldRotation => Quaternion.Euler(holdRotation);

        public void Hold()
        {
            Held?.Invoke();
        }

        public void Release(ReleaseData data)
        {
            Released?.Invoke(data);
        }
    }
}
