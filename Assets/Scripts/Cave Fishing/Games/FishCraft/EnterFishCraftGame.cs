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
        [SerializeField] private Instructor instructor;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Color fogColor;

        public override void Enable()
        {
            gameObject.SetActive(true);

            player.Fisher.Disable();
            player.Character.SetPosition(spawnPoint.position);
            interactor.Enable();
            fishCraftPlayer.Enable();

            RenderSettings.fogColor = fogColor;

            SignalShuttle.Emit(new GameEnabledSignal(MinigameType.FishCraft));

            instructor.Instruct(null);
            Disable();
        }

        public override void Disable()
        {
            SignalShuttle.Emit(new GameDisabledSignal());
        }
    }
}
