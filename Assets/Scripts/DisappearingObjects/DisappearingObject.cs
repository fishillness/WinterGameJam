using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class DisappearingObject : TriggerCollider
    {
        public event UnityAction<DisappearingObject> OnObjectDestroy;
        
        [Header("Particles System")]
        [SerializeField] private bool PlayParticles;
        [SerializeField] private GameObject particlesPrefab;
        [SerializeField] private float appearanceHeight;

        [Header("Sound")]
        [SerializeField] private bool PlaySound;
        [SerializeField] private SoundType soundType;

        private DisappearingObject disappearingObject;
        
        [SerializeField] private SoundPlayer soundPlayer;

        private void Start()
        {
            disappearingObject = GetComponent<DisappearingObject>();
        }

        protected override void OnPlayerEnter()
        {
            OnObjectDestroy?.Invoke(disappearingObject);

            if (PlayParticles)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + appearanceHeight, transform.position.z);
                Instantiate(particlesPrefab, pos, transform.rotation);
            }

            if (PlaySound)
            {
                SoundPlayer.Instance.Play(soundType);
                //SoundPlayer.instance.Play(soundType);
            }

            Destroy(gameObject);
        }

        public virtual void SetParameters() { }
    }
}
