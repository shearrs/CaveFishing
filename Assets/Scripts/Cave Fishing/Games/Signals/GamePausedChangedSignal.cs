using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games
{
    public readonly struct GamePausedChangedSignal : ISignal
    {
        private readonly bool isPaused;

        public readonly bool IsPaused => isPaused;

        public GamePausedChangedSignal(bool isPaused) => this.isPaused = isPaused;
    }
}
