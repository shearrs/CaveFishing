using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Pong : Minigame
    {
        [SerializeField] private Player player;
        [SerializeField] private Bot bot;
        [SerializeField] private Ball ball;

        private void Start()
        {
            Enable();
        }

        public override void Enable()
        {
            player.Enable();
            ball.SetPosition(new(0.5f, 0.5f));
            ball.Enable();
        }

        public override void Disable()
        {
            player.Disable();
            ball.Disable();
        }
    }
}
