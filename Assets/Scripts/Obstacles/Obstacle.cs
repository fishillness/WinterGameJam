using UnityEngine;

namespace WinterGameJam
{
    public class Obstacle : TriggerCollider, IDependency<SpeedControl>, IDependency<Player>
    {
        [SerializeField] private float rewindTime;

        private Timer timer;
        private bool isTimerWork;

        private SpeedControl speedControl;
        private Player player;
        public void Construct(SpeedControl obj) => speedControl = obj;
        public void Construct(Player obj) => player = obj;

        private void Start()
        {
            InitTimers(rewindTime);
        }

        private void Update()
        {
            UpdateTimers();
        }

        protected override void OnPlayerEnter()
        {
            speedControl.SetRewindSpeed();
            StartTimer(rewindTime);
        }

        private void InitTimers(float rewindTime)
        {
            timer = new Timer(rewindTime);
        }

        private void StartTimer(float rewindTime)
        {
            timer.StartTimer(rewindTime);
            isTimerWork = true;
            player.SetControlUnavailable();
        }

        private void UpdateTimers()
        {
            if (timer.IsFinished)
            {
                isTimerWork = false;
                speedControl.SetFirstSpeed();
                player.SetControlAvailable();
            }

            if (isTimerWork)
                timer.RemoveTime(Time.deltaTime);
        }
    }
}
