using CaveFishing.Games;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter character;

        private void OnEnable()
        {
            SignalShuttle.Register<GameEnabledSignal>(OnGameEnabled);
            SignalShuttle.Register<GameDisabledSignal>(OnGameDisabled);
        }

        private void OnDisable()
        {
            SignalShuttle.Deregister<GameEnabledSignal>(OnGameEnabled);
            SignalShuttle.Deregister<GameDisabledSignal>(OnGameDisabled);
        }

        private void OnGameEnabled(GameEnabledSignal signal)
        {
            character.Disable();
        }

        private void OnGameDisabled(GameDisabledSignal signal)
        {
            character.Enable();
        }
    }
}
