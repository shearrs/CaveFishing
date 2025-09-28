using System;
using UnityEngine;

namespace CaveFishing.Items
{
    public class Item : MonoBehaviour, IItem
    {
        public event Action Held;
        public event Action Released;

        public void Hold()
        {
            Held?.Invoke();
        }

        public void Release()
        {
            Released?.Invoke();
        }
    }
}
