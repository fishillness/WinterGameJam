using UnityEngine;

namespace WinterGameJam
{
    public class Timer : MonoBehaviour
    {
        private float currentTime;

        public bool IsFinished => currentTime <= 0;
        public float CurrentTime => currentTime;

        public Timer(float startTime)
        {
            currentTime = startTime;
        }

        public void StartTimer(float startTime)
        {
            currentTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (currentTime <= 0) return;

            currentTime -= deltaTime;
        }

        public void AddTime(float time)
        {
            currentTime += time;
        }
    }
}
