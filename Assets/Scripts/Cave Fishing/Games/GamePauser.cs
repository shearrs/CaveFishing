using Shears;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private Pauser pauser;

        public void TogglePause()
        {
            pauser.TogglePause();

            SignalShuttle.Emit(new GamePausedChangedSignal(pauser.IsPaused));
        }
    }
}
