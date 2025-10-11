using UnityEngine;

namespace CaveFishing.Audio
{
    public class CaveAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip ambientSound;

        private void Start()
        {
            AudioManager.PlayMusic(ambientSound, volume: 0.1f);
        }
    }
}
