using Shears;
using Shears.Signals;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Pong : Minigame
    {
        [Header("Components")]
        [SerializeField] private Player player;
        [SerializeField] private Bot bot;
        [SerializeField] private Ball ball;

        [Header("Score")]
        [SerializeField, Min(1)] private int scoreToWin = 3;
        [SerializeField, ReadOnly] private int playerScore = 0;
        [SerializeField, ReadOnly] private int botScore = 0;

        public int PlayerScore => playerScore;
        public int BotScore => botScore;

        public event Action Enabled;
        public event Action Disabled;
        public event Action ScoreUpdated;

        private void OnEnable()
        {
            ball.HitLeftSide += OnHitLeftSide;
            ball.HitRightSide += OnHitRightSide;
        }

        private void OnDisable()
        {
            ball.HitLeftSide -= OnHitLeftSide;
            ball.HitRightSide -= OnHitRightSide;
        }

        public override void Enable()
        {
            StartCoroutine(IESpawnBall());

            playerScore = 0;
            botScore = 0;

            player.Enable();
            bot.Enable();

            Enabled?.Invoke();
            SignalShuttle.Emit(new GameEnabledSignal());
        }

        public override void Disable()
        {
            ball.Disable();
            player.Disable();
            bot.Disable();

            Disabled?.Invoke();
            SignalShuttle.Emit(new GameDisabledSignal());
        }

        private void OnHitRightSide()
        {
            playerScore++;
            ball.Disable();

            ScoreUpdated?.Invoke();

            if (playerScore == scoreToWin)
            {
                Disable();
                SignalShuttle.Emit(new GameWonSignal());
            }
            else
                StartCoroutine(IESpawnBall());
        }

        private void OnHitLeftSide()
        {
            botScore++;
            ball.Disable();

            ScoreUpdated?.Invoke();

            if (botScore == scoreToWin)
            {
                Disable();
                SignalShuttle.Emit(new GameLostSignal());
            }
            else
                StartCoroutine(IESpawnBall());
        }

        private IEnumerator IESpawnBall()
        {
            yield return CoroutineUtil.WaitForSeconds(1.5f);

            ball.SetPosition(new(0.5f, 0.5f));
            ball.Enable();
        }
    }
}
