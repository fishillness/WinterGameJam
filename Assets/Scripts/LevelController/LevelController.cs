using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class LevelController : MonoBehaviour
    {

        public static event UnityAction TimerIsOver;

        [SerializeField] private float levelDurationTime;

        private Timer timer;
        private bool isTimerActive;
        private float allTime;

        public float CurrentTime => timer.CurrentTime;

        private void Start()
        {
            InitTimers(levelDurationTime);
            StartTimer(levelDurationTime);

            allTime = levelDurationTime;
        }

        private void Update()
        {
            UpdateTimers();
        }

        private void InitTimers(float time)
        {
            timer = new Timer(time);
        }

        private void StartTimer(float time)
        {
            timer.StartTimer(time);
            isTimerActive = true;
        }

        private void UpdateTimers()
        {
            if (isTimerActive == false) return;

            timer.RemoveTime(Time.deltaTime);

            if (timer.IsFinished)
            {
                isTimerActive = false;
                TimerIsOver?.Invoke();
            }
        }

        public void AddTime(float value)
        {
            timer.AddTime(value);
            allTime += value;
        }

        public float SpentTime()
        {
            return (allTime - timer.CurrentTime);
        }
    }
}
