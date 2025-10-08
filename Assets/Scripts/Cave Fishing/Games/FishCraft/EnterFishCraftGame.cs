using CaveFishing.Players;
using Shears.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class EnterFishCraftGame : Minigame
    {
        [Header("Player")]
        [SerializeField] private Player player;
        [SerializeField] private FishCraftPlayer fishCraftPlayer;
        [SerializeField] private BlockInteractor interactor;

        [Header("Game")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private List<Block> blocks;
        [SerializeField] private Color fogColor;

        public override void Enable()
        {
            foreach (var block in blocks)
                block.ResetBlock();

            player.Fisher.Disable();

            player.Character.SetPosition(spawnPoint.position);
            interactor.Enable();
            fishCraftPlayer.Enable();

            RenderSettings.fogColor = fogColor;

            SignalShuttle.Emit(new GameEnabledSignal(MinigameType.FishCraft));

            Disable();
        }

        public override void Disable()
        {
            SignalShuttle.Emit(new GameDisabledSignal());
        }
    }
}
