using Shears;
using Shears.Signals;
using UnityEngine;
using UnityEngine.Events;

namespace CaveFishing.Games
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private Pauser pauser;
        [SerializeField] private UnityEvent onPaused;
        [SerializeField] private UnityEvent onUnpaused;

        private CursorLockMode lockMode;
        private bool isCursorVisible = false;

        public void TogglePause()
        {
            pauser.TogglePause();

            SignalShuttle.Emit(new GamePausedChangedSignal(pauser.IsPaused));

            if (pauser.IsPaused)
            {
                lockMode = Cursor.lockState;
                isCursorVisible = Cursor.visible;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                onPaused.Invoke();
            }
            else
            {

                Cursor.lockState = lockMode;
                Cursor.visible = isCursorVisible;
                onUnpaused.Invoke();
            }
        }
    }
}
