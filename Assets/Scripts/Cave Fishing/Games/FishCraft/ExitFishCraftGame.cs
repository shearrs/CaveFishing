using CaveFishing.Players;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class ExitFishCraftGame : Minigame
    {
        [SerializeField] private PlayerCharacter character;
        [SerializeField] private Transform returnPoint;

        public override void Enable()
        {
            character.transform.position = returnPoint.transform.position;

            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            SignalShuttle.Emit(new GameWonSignal());
            SignalShuttle.Emit(new GameDisabledSignal());
        }
    }
}
