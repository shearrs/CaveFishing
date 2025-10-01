using Shears;
using System;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame
{
    public class SlidingBar : MonoBehaviour
    {
        [SerializeField] private float risingSpeed = 2.0f;
        [SerializeField] private float fallSpeed = 2.0f;
        [SerializeField, ReadOnly] private bool isMovingUp = false;
        [SerializeField, ReadOnly, Range(0f, 1f)] private float progress = 0f;

        private float velocity = 0f;
        private bool isEnabled = false;

        public event Action<float> ProgressUpdated;

        public void Enable()
        {
            SetProgress(0.4f);
            velocity = 0f;
            isEnabled = true;
        }

        public void Disable()
        {
            SetProgress(0f);
            isEnabled = false;
        }

        private void Update()
        {
            if (!isEnabled)
                return;

            if (isMovingUp)
                isMovingUp = false;
            else
                MoveDown();

            if (velocity < 0.0f && progress == 0.0f)
                velocity = 0.0f;
            else if (velocity > 0.0f && progress == 1.0f)
                velocity = 0.0f;

            SetProgress(progress + velocity);
        }

        public void MoveUp()
        {
            velocity += Time.deltaTime * Time.deltaTime * risingSpeed;

            isMovingUp = true;
        }

        private void MoveDown()
        {
            velocity -= Time.deltaTime * Time.deltaTime * fallSpeed;
        }

        private void SetProgress(float value)
        {
            progress = Mathf.Clamp01(value);
            ProgressUpdated?.Invoke(progress);
        }
    }
}
