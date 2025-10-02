using Shears;
using System;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Ball : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Vector2 progress;
        [SerializeField] private float speed;

        private Vector2 currentDirection;
        private bool isEnabled = false;

        public event Action<Vector2> ProgressUpdated;

        public void Enable()
        {
            if (isEnabled)
                return;

            StartCoroutine(IEUpdate());

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            StopAllCoroutines();

            isEnabled = false;
        }

        public void SetPosition(Vector2 position)
        {
            progress = position;

            ProgressUpdated?.Invoke(progress);
        }
        
        private IEnumerator IEUpdate()
        {
            currentDirection = new Vector2(0.5f, 1).normalized;

            while (true)
            {
                Vector2 position = progress;
                position += speed * Time.deltaTime * currentDirection;
                position = position.ClampComponents(0, 1);

                if (position.y == 1 || position.y == 0)
                    currentDirection.y = -currentDirection.y;
                if (position.x == 1 || position.x == 0)
                    currentDirection.x = -currentDirection.x;

                SetPosition(position);

                yield return null;
            }
        }
    }
}
