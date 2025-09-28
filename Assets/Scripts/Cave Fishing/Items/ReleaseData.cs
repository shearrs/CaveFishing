using UnityEngine;

namespace CaveFishing.Items
{
    public readonly struct ReleaseData
    {
        private readonly Vector3 releaseVelocity;

        public readonly Vector3 ReleaseVelocity => releaseVelocity;

        public ReleaseData(Vector3 releaseVelocity)
        {
            this.releaseVelocity = releaseVelocity;
        }
    }
}
