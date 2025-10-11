using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.QuickClickGame
{
    public readonly struct TargetClickedSignal : ISignal
    {
        private readonly ClickTarget target;

        public readonly ClickTarget Target => target;

        public TargetClickedSignal(ClickTarget target) { this.target = target; }
    }
}
