using UnityEngine.Events;

namespace WinterGameJam
{
    public class Santa : TriggerCollider 
    {
        public static event UnityAction OnCaughtSanta;
        public static event UnityAction OnPlayerEnterSanta;
        public static event UnityAction OnPlayerExitSanta;

        private void Start()
        {
            InputControll.OnPressedInteractButton += Check;
        }

        private void OnDestroy()
        {
            InputControll.OnPressedInteractButton -= Check;
        }

        private void Check()
        {
            if (IsPlayerEnter)
            {
                OnCaughtSanta?.Invoke();
                OnPlayerExitSanta?.Invoke();
            }
        }

        protected override void OnPlayerEnter()
        {
            base.OnPlayerEnter();
            OnPlayerEnterSanta?.Invoke();
        }

        protected override void OnPlayerExit()
        {
            base.OnPlayerExit();
            OnPlayerExitSanta?.Invoke();
        }
    }
}
