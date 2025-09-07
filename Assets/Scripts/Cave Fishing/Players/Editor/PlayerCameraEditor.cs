using UnityEngine;
using UnityEditor;
using ShearsLibrary.Cameras;

namespace CaveFishing.Players.Editor
{
    [CustomEditor(typeof(PlayerCamera))]
    public class PlayerCameraEditor : UnityEditor.Editor
    {
        private ManagedCamera managedCamera;
        private FirstPersonCameraState firstPersonState;

        private void OnEnable()
        {
            var playerCamera = (PlayerCamera)target;

            managedCamera = playerCamera.GetComponent<ManagedCamera>();
            firstPersonState = playerCamera.GetComponent<FirstPersonCameraState>();

            managedCamera.hideFlags = HideFlags.HideInInspector;
            firstPersonState.hideFlags = HideFlags.HideInInspector;
        }

        private void OnDisable()
        {
            var playerCamera = (PlayerCamera)target;

            if (playerCamera != null)
                return;

            DestroyImmediate(managedCamera);
            DestroyImmediate(firstPersonState);
        }
    }
}
