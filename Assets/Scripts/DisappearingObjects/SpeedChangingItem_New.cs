using UnityEngine;

namespace WinterGameJam
{
    public class SpeedChangingItem_New : DisappearingObject, IDependency<RoadGenerator>
    {
        [Header("Speed")]
        [SerializeField] protected float valueSpeed—hange;

        private RoadGenerator roadGenerator;
        public void Construct(RoadGenerator obj) => roadGenerator = obj;

        protected override void OnPlayerEnter()
        {
            roadGenerator.ChangeSpeed(valueSpeed—hange);
            base.OnPlayerEnter();
        }

        public void SetRoadGenerator(RoadGenerator generator)
        {
            roadGenerator = generator;
        }
    }
}
