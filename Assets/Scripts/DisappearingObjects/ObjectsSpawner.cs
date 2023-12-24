using UnityEngine;

namespace WinterGameJam
{
    public class ObjectsSpawner : MonoBehaviour, IDependency<SpeedControl>
    {
        [SerializeField] private DisappearingObject prefab;
        [SerializeField] private float respawnTime;

        private Timer timer;
        private bool isTimerWork;

        private SpeedControl speedControl;
        public void Construct(SpeedControl obj) => speedControl = obj;

        private void Start()
        {
            InitTimers(respawnTime);

            DisappearingObject firstObject = GetComponentInChildren<DisappearingObject>();

            if (firstObject == null)
                SpawnObject();
            else
                firstObject.OnObjectDestroy += ObjectDestroy;
        }

        private void Update()
        {
            UpdateTimers();
        }

        private void SpawnObject()
        {
            DisappearingObject obj = Instantiate(prefab, transform.position, transform.rotation, transform);
            obj.SetParameters();
            obj.OnObjectDestroy += ObjectDestroy;
        }

        private void ObjectDestroy(DisappearingObject obj)
        {
            obj.OnObjectDestroy -= ObjectDestroy;
            StartTimer(respawnTime);
        }

        private void InitTimers(float time)
        {
            timer = new Timer(time);
        }

        private void StartTimer(float time)
        {
            timer.StartTimer(time);
            isTimerWork = true;
        }

        private void UpdateTimers()
        {
            if (timer.IsFinished)
            {
                isTimerWork = false;
                SpawnObject();
            }

            if (isTimerWork)
                timer.RemoveTime(Time.deltaTime);
        }
    }
}
