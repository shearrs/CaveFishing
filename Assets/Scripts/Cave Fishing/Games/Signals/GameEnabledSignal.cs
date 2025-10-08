using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games
{
    public readonly struct GameEnabledSignal : ISignal
    {
        private readonly MinigameType type;

        public readonly MinigameType Type => type;

        public GameEnabledSignal(MinigameType type)
        { 
            this.type = type; 
        }
    }
}
