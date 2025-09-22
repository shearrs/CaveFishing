using Shears;
using UnityEngine;

namespace CaveFishing.Games.FishBarGame
{
    public class SlidingBar : MonoBehaviour
    {
        [SerializeField, ReadOnly, Range(0f, 1f)] private float progress = 0f;

        public void Enable()
        {

        }

        public void Disable()
        {

        }

        private void Update()
        {

        }

        public void MoveUp(float amount)
        {
            progress += amount;
        }

        private void MoveDown()
        {

        }
    }
}
