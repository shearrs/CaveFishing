using Shears.Logging;
using UnityEngine;

namespace CaveFishing.Games
{
    public abstract class Minigame : SHMonoBehaviourLogger
    {
        public abstract void Enable();

        public abstract void Disable();
    }
}
