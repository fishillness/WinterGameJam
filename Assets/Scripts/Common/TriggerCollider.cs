using UnityEngine;

namespace WinterGameJam
{
    public abstract class TriggerCollider : MonoBehaviour
    {
        [Header("DEBUG")]
        [SerializeField] private bool isPlayerEnter;
        public bool IsPlayerEnter => isPlayerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerEnter = true;
                OnPlayerEnter();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerEnter = false;
                OnPlayerExit();
            }
        }

        protected virtual void OnPlayerEnter() { }
        protected virtual void OnPlayerExit() { }
    }
}
