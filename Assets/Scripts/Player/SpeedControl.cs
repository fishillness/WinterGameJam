using UnityEngine;

namespace WinterGameJam
{
    [RequireComponent(typeof(Animator))]
    public class SpeedControl : MonoBehaviour
    {
        [SerializeField] private float step;
        [SerializeField] private float maxSpeedIncrease;
        [SerializeField] private float minSpeedReduction;
        [SerializeField] private float rewindSpeed;

        private Animator animator;
        private string nameParameter = "MoveSpeed";
        private float firstSpeed;

        //public float CurentSpeed => animator.GetFloat(nameParameter);

        private void Start()
        {
            animator = GetComponent<Animator>();
            firstSpeed = animator.GetFloat(nameParameter);
        }

        public void SpeedUp()
        {
            float speed = animator.GetFloat(nameParameter) + step;

            if (speed > (firstSpeed + maxSpeedIncrease))
                speed = firstSpeed + maxSpeedIncrease;

            animator.SetFloat(nameParameter, speed);
        }

        public void SpeedDown()
        {
            float speed = animator.GetFloat(nameParameter) - step;

            if (speed < (firstSpeed - minSpeedReduction)) 
                speed = Mathf.Clamp(firstSpeed - minSpeedReduction, 0, firstSpeed);

            if (speed < 0.1f)
                speed = 0.1f;

            animator.SetFloat(nameParameter, speed);
        }

        public void SetFirstSpeed()
        {
            animator.SetFloat(nameParameter, firstSpeed);
        }

        public void Stop()
        {
            animator.SetFloat(nameParameter, 0);
        }

        public void SetRewindSpeed()
        {
            animator.SetFloat(nameParameter, rewindSpeed);
        }
    }
}