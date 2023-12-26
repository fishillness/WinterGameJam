using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class SantaClaus : TriggerCollider, IDependency<RoadGenerator>
    {
        public static event UnityAction OnCaughtSanta;

        [SerializeField] private float maxRemovalLength = 15;
        [SerializeField] private float minRemovalLength = -2;

        private float maxSpeed;
        private float minSpeed;

        private RoadGenerator roadGenerator;
        public void Construct(RoadGenerator obj) => roadGenerator = obj;

        private void Start()
        {
            maxSpeed = roadGenerator.MaxSpeed;
            minSpeed = roadGenerator.MinSpeed;

            transform.position = new Vector3(transform.position.x, transform.position.y, maxRemovalLength);
        }

        private void FixedUpdate()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
                maxRemovalLength - CalculateLength(roadGenerator.currentSpeed));
        }

        private float CalculateLength(float currentSpeed)
        {
            float percentageOfSpeedAchieved = 100 * (currentSpeed - minSpeed) / (maxSpeed - minSpeed);
            float newPosition = percentageOfSpeedAchieved * (maxRemovalLength - minRemovalLength) / 100;

            return newPosition;
        }

        protected override void OnPlayerEnter()
        {
            OnCaughtSanta?.Invoke();
            base.OnPlayerEnter();
        }
    }
}
