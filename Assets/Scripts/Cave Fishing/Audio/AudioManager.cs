using Shears;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Audio
{
    public class AudioManager : ProtectedSingleton<AudioManager>
    {
        [SerializeField] private AudioSource musicSource0;
        [SerializeField] private AudioSource musicSource1;

        private AudioSource currentMusicSource;

        public static void PlayMusic(AudioClip clip, float fadeDuration = 1.5f, float volume = 1.0f) => Instance.InstPlayMusic(clip, fadeDuration, volume);

        private void InstPlayMusic(AudioClip clip, float fadeDuration = 1.5f, float volume = 1.0f)
        {
            if (currentMusicSource == null || !currentMusicSource.isPlaying)
            {
                var source = GetOtherMusicSource();
                source.volume = volume;

                source.PlayOneShot(clip);
            }
            else
                StartCoroutine(IEFadeTo(clip, fadeDuration, volume));
        }

        public static void StopMusic() => Instance.InstStopMusic();

        private void InstStopMusic()
        {
            StopAllCoroutines();
            musicSource0.Stop();
            musicSource1.Stop();
        }

        public static void FadeOutMusic(float duration = 1.0f) => Instance.InstFadeOutMusic(duration);

        private void InstFadeOutMusic(float duration = 1.0f)
        {
            if (currentMusicSource == null || !currentMusicSource.isPlaying)
                return;

            StopAllCoroutines();
            StartCoroutine(IEFadeOut(duration));
        }

        private IEnumerator IEFadeTo(AudioClip clip, float fadeDuration, float targetVolume)
        {
            float elapsedTime = 0;
            AudioSource fromSource = currentMusicSource;
            AudioSource toSource = GetOtherMusicSource();
            float fromVolume = fromSource.volume;

            currentMusicSource = toSource;
            toSource.PlayOneShot(clip);

            while (elapsedTime < fadeDuration)
            {
                float t = elapsedTime / fadeDuration;

                fromSource.volume = Mathf.Lerp(fromVolume, 0, t);
                toSource.volume = Mathf.Lerp(0, targetVolume, t);

                yield return null;
            }

            fromSource.Stop();
        }

        private IEnumerator IEFadeOut(float duration)
        {
            float elapsedTime = 0;
            float fromVolume = currentMusicSource.volume;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                currentMusicSource.volume = Mathf.Lerp(fromVolume, 0, t);

                yield return null;
            }

            currentMusicSource.Stop();
        }

        private AudioSource GetOtherMusicSource() => currentMusicSource == musicSource0 ? musicSource1 : musicSource0;
    }
}
