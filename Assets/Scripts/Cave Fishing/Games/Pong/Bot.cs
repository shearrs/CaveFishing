using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private Paddle paddle;

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
            yield return null;
        }
    }
}
