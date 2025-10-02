using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Pong : Minigame
    {
        [SerializeField] private Player player;
        [SerializeField] private Bot bot;

        private void Start()
        {
            Enable();
        }

        public override void Enable()
        {
            player.Enable();
        }

        public override void Disable()
        {
            player.Disable();
        }
    }
}
