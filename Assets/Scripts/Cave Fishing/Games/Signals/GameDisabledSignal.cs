using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games
{
    public readonly struct GameDisabledSignal : ISignal
    {
        private readonly MinigameType type;

        public readonly MinigameType Type => type;

        public GameDisabledSignal(MinigameType type)
        {
            this.type = type;
        }
    }
}
