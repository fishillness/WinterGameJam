using UnityEngine;

namespace WinterGameJam
{
    public class DisappearingThroughTime : MonoBehaviour
    {
        [SerializeField] private float timeThroughObjectDisappearing;

        private Timer timer;

        private void Start()
        {
            InitTimers(timeThroughObjectDisappearing);
            StartTimer(timeThroughObjectDisappearing);
        }

        private void Update()
        {
            UpdateTimers();

            if (timer.IsFinished)
                Disappear();

        }

        private void Disappear()
        {
            Destroy(gameObject);
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
