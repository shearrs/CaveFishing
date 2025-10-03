using Shears;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float startingSpeed = 0.1f;
        [SerializeField] private float speedIncrease = 0.01f;
        [SerializeField, ReadOnly] private float currentSpeed;
        [SerializeField, ReadOnly] private Vector2 progress;

        private Vector2 currentDirection;
        private bool isEnabled = false;

        public Vector2 Position => progress;
        public Vector2 Velocity => currentDirection;

        public event Action<Vector2> ProgressUpdated;
        public event Action HitRightSide;
        public event Action HitLeftSide;
        public event Action Enabled;
        public event Action Disabled;

        public void Enable()
        {
            if (isEnabled)
                return;

            isEnabled = true;
            Enabled?.Invoke();

            StartCoroutine(IEUpdate());
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            StopAllCoroutines();

            isEnabled = false;
            Disabled?.Invoke();
        }

        public void SetPosition(Vector2 position)
        {
            progress = position;

            ProgressUpdated?.Invoke(progress);
        }
        
        private IEnumerator IEUpdate()
        {
            currentDirection = new Vector2(0.5f, 1).normalized;
            currentSpeed = startingSpeed;

            while (true)
            {
                Vector2 position = progress;
                position += currentSpeed * Time.deltaTime * currentDirection;
                position = position.ClampComponents(0, 1);

                if (position.y == 1 || position.y == 0)
                    currentDirection.y = -currentDirection.y;

                if (position.x == 1)
                    HitRightSide?.Invoke();
                else if (position.x == 0)
                    HitLeftSide?.Invoke();

                SetPosition(position);

                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentDirection.x = -currentDirection.x;
            currentSpeed += speedIncrease;
        }
    }
}
