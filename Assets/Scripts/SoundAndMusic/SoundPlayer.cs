using UnityEngine;

namespace WinterGameJam
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private SoundProperties soundProperties;

        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(SoundType soundType)
        {
            audioSource.PlayOneShot(soundProperties[soundType]);

        }
    }
}
