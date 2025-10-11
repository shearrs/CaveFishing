using Shears;
using UnityEngine;

namespace CaveFishing.Players
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private Pauser pauser;
        [SerializeField] private PlayerCamera playerCamera;

        private bool cameraIsEnabled = false;

        public void TogglePause()
        {
            pauser.TogglePause();

            if (pauser.IsPaused)
            {
                cameraIsEnabled = playerCamera.IsEnabled;
                playerCamera.Disable();
            }
            else
            {
                if (cameraIsEnabled)
                {
                    cameraIsEnabled = false;
                    playerCamera.Enable();
                }
            }
        }
    }
}
