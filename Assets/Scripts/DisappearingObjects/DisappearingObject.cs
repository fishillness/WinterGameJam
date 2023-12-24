using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class DisappearingObject : TriggerCollider
    {
        public event UnityAction<DisappearingObject> OnObjectDestroy;

        [SerializeField] private DisappearingObject disappearingObject;

        private void Start()
        {
            disappearingObject = GetComponent<DisappearingObject>();
        }

        protected override void OnPlayerEnter()
        {
            OnObjectDestroy?.Invoke(disappearingObject);
            Destroy(gameObject);
        }

        public virtual void SetParameters() { }
    }
}
