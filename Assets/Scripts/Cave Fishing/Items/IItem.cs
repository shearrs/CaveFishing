using UnityEngine;

namespace CaveFishing.Items
{
    public interface IItem
    {
#pragma warning disable IDE1006 // Naming Styles
        public GameObject gameObject { get; }
        public Transform transform { get; }
#pragma warning restore IDE1006 // Naming Styles

        public void Hold();
        public void Release(ReleaseData data);
    }
}
