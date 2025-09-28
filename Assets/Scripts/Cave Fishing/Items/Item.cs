using System;
using UnityEngine;

namespace CaveFishing.Items
{
    public class Item : MonoBehaviour, IItem
    {
        public event Action Held;
        public event Action<ReleaseData> Released;

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
