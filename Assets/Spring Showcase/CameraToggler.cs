using Shears.Cameras;
using UnityEngine;

namespace CaveFishing
{
    public class CameraToggler : MonoBehaviour
    {
        [SerializeField] private ManagedCamera cam;
        [SerializeField] private ThirdPersonCameraState state;

        private bool firstPerson = true;

        public void Toggle()
        {
            if (firstPerson)
            {
                cam.SetState<ThirdPersonCameraState>();
                firstPerson = false;
            }
            else
            {
                cam.SetState<FirstPersonCameraState>();
                firstPerson = true;
            }
        }

        public void IncreaseSmoothing()
        {
            state.Smoothing += 10.0f;

            if (state.Smoothing > 100.0f)
                state.Smoothing = 100.0f;
        }

        public void DecreaseSmoothing()
        {
            state.Smoothing -= 10.0f;

            if (state.Smoothing < 5.0f)
                state.Smoothing = 5.0f;
        }

        public void ResetSmoothing()
        {
            state.Smoothing = 100.0f;
        }
    }
}
