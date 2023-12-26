using UnityEngine;

namespace WinterGameJam
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private float LevelDurationTime;

        private Timer timer;

        public float CurrentTime => timer.CurrentTime;

        private void Start()
        {
            InitTimers(LevelDurationTime);
            StartTimer(LevelDurationTime);
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
        }

        private void UpdateTimers()
        {
            timer.RemoveTime(Time.deltaTime);
        }

    }
}
