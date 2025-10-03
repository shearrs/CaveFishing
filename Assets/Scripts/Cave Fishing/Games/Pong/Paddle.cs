using Shears;
using System;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField, ReadOnly] private float progress = 0;

        public float Position => progress;

        public event Action<float> ProgressUpdated;

        public void MoveUp(float amount)
        {
            SetPosition(progress + amount);
        }

        public void MoveDown(float amount)
        {
            SetPosition(progress - amount);
        }

        public void SetPosition(float position)
        {
            progress = Mathf.Clamp01(position);
            ProgressUpdated?.Invoke(progress);
        }
    }
}
