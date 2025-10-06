using CaveFishing.Players;
using Shears.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class EnterFishCraftGame : Minigame
    {
        [SerializeField] private PlayerCharacter character;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private List<Block> blocks;

        public override void Enable()
        {
            foreach (var block in blocks)
                block.ResetBlock();

            character.transform.position = spawnPoint.position;

            SignalShuttle.Emit(new GameEnabledSignal());

            Disable();
        }

        public override void Disable()
        {
            SignalShuttle.Emit(new GameDisabledSignal());
        }
    }
}
