using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class Pauser : MonoBehaviour
    {
        public static event UnityAction<bool> PauseStateChange;

        private bool isPause;
        public bool IsPause => isPause;

        private void Start()
        {
            InputControll.OnPressedPause += ChangePauseState;
        }

        private void OnDestroy()
        {
            InputControll.OnPressedPause -= ChangePauseState;
        }

        public void Pause()
        {
            if (isPause == true) return;

            Time.timeScale = 0;
            isPause = true;
            PauseStateChange?.Invoke(isPause);
        }

        public void UnPause()
        {
            if (isPause == false) return;

            Time.timeScale = 1;
            isPause = false;
            PauseStateChange?.Invoke(isPause);
        }

        public void ChangePauseState()
        {
            if (isPause == true)
                UnPause();
            else
                Pause();
        }

    }
}
