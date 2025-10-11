using CaveFishing.Audio;
using Shears.Signals;
using UnityEngine;

namespace CaveFishing.Games
{
    public class GameMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip minigameMusic;
        [SerializeField] private AudioClip fishCraftMusic;
        [SerializeField] private AudioClip ambientSound;

        private void Start()
        {
            AudioManager.PlayMusic(ambientSound, volume: 0.1f);
        }

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
            if (signal.Type == MinigameType.QuickClick || signal.Type == MinigameType.FishTyping)
                AudioManager.PlayMusic(minigameMusic, volume: 0.1f);
            else if (signal.Type == MinigameType.FishCraft)
                AudioManager.PlayMusic(fishCraftMusic, volume: 0.3f);
        }

        private void OnGameDisabled(GameDisabledSignal signal)
        {
            if (signal.Type == MinigameType.QuickClick || signal.Type == MinigameType.FishTyping || signal.Type == MinigameType.ExitFishCraft)
                AudioManager.PlayMusic(ambientSound, volume: 0.1f);
        }
    }
}
