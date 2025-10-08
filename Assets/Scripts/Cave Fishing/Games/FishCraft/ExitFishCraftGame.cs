using CaveFishing.Players;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public class ExitFishCraftGame : Minigame
    {
        [Header("Player")]
        [SerializeField] private Player player;
        [SerializeField] private FishCraftPlayer fishCraftPlayer;
        [SerializeField] private BlockInteractor interactor;

        [Header("Game")]
        [SerializeField] private Transform returnPoint;

        private Color originalFogColor;

        private void Awake()
        {
            originalFogColor = RenderSettings.fogColor;
        }

        public override void Enable()
        {
            player.Character.SetPosition(returnPoint.position);
            interactor.Disable();
            fishCraftPlayer.Disable();
            transform.parent.gameObject.SetActive(false);

            RenderSettings.fogColor = originalFogColor;

            SignalShuttle.Emit(new GameEnabledSignal());

            Disable();
        }

        public override void Disable()
        {
            SignalShuttle.Emit(new GameWonSignal());
            SignalShuttle.Emit(new GameDisabledSignal(MinigameType.FishCraft));
        }
    }
}
