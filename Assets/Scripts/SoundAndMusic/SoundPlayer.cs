using UnityEngine;

namespace WinterGameJam
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        static public SoundPlayer instance;

        [SerializeField] private SoundProperties soundProperties;

        private AudioSource audioSource;

        private void Awake()
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(SoundType soundType)
        {
            audioSource.PlayOneShot(soundProperties[soundType]);

        }
    }
}
