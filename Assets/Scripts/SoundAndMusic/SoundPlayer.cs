using UnityEngine;

namespace WinterGameJam
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundProperties soundProperties;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(SoundType soundType)
        {
            audioSource.PlayOneShot(soundProperties[soundType]);
        }
    }
}
