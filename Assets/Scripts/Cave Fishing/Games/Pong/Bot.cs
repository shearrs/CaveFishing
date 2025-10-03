using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Bot : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Paddle paddle;
        [SerializeField] private Ball ball;

        [Header("Settings")]
        [SerializeField] private float safeSpeed = 1.0f;
        [SerializeField] private float dangerousSpeed = 2.0f;

        private bool isEnabled = false;

        public void Enable()
        {
            if (isEnabled)
                return;

            paddle.SetPosition(0.5f);
            StartCoroutine(IEControl());

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            StopAllCoroutines();

            isEnabled = false;
        }

        private IEnumerator IEControl()
        {
            while (true)
            {
                bool isDangerous = ball.Position.x > 0.5f && ball.Velocity.x > 0;
                float speed = isDangerous ? dangerousSpeed : safeSpeed;
                float position = Mathf.MoveTowards(paddle.Position, ball.Position.y, speed * Time.deltaTime);
                
                paddle.SetPosition(position);

                yield return null;
            }
        }
    }
}
