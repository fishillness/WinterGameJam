using UnityEngine;

namespace WinterGameJam
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private MusicProperties musicProperties;
        [SerializeField] private MusicType musicType;
        [SerializeField] private bool playOnAwake;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (playOnAwake)
            {
                Play(musicType);
            }
        }

        public void Play(MusicType musicType)
        {
            audioSource.clip = musicProperties[musicType];
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}
