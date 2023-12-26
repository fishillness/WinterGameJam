using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class LevelController : MonoBehaviour
    {

        public static event UnityAction TimerIsOver;

        [SerializeField] private float levelDurationTime;

        private Timer timer;
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

            if (timer.IsFinished)
            {
                TimerIsOver?.Invoke();
            }
        }

        private void InitTimers(float time)
        {
            timer = new Timer(time);
        }

        private void StartTimer(float time)
        {
            timer.StartTimer(time);
        }

        private void UpdateTimers()
        {
            timer.RemoveTime(Time.deltaTime);
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
