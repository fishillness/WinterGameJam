using UnityEngine;

namespace WinterGameJam
{
    public class SpeedChangingItem_New : DisappearingObject, IDependency<RoadGenerator_New>
    {
        [SerializeField] protected float valueSpeed—hange;

        private RoadGenerator_New roadGenerator;
        public void Construct(RoadGenerator_New obj) => roadGenerator = obj;

        protected override void OnPlayerEnter()
        {
            roadGenerator.ChangeSpeed(valueSpeed—hange);
            base.OnPlayerEnter();
        }
    }
}
