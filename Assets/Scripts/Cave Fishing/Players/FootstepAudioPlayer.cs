using Shears;
using UnityEngine;

namespace CaveFishing.Players
{
    public class FootstepAudioPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerCamera cam;
        [SerializeField] private PlayerCharacter character;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Range<float> pitchRange = new(0.85f, 1.15f);

        private void OnEnable()
        {
            cam.SteppedDown += OnSteppedDown;
        }

        private void OnDisable()
        {
            cam.SteppedDown -= OnSteppedDown;
        }

        private void OnSteppedDown()
        {
            if (!character.IsGrounded)
                return;

            audioSource.pitch = pitchRange.Random();
            audioSource.Play();
        }
    }
}
