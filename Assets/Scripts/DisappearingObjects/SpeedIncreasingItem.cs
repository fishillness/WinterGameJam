using UnityEngine;

namespace WinterGameJam
{
    public class SpeedIncreasingItem : DisappearingObject, IDependency<SpeedControl>
    {
        private SpeedControl speedControl;
        public void Construct(SpeedControl obj) => speedControl = obj;

        protected override void OnPlayerEnter()
        {
            speedControl.SpeedUp();
            base.OnPlayerEnter();
        }

        public void SetSpeedControl(SpeedControl speedControl)
        {
            this.speedControl = speedControl;
        }

        public override void SetParameters()
        {
            speedControl = GameObject.FindAnyObjectByType<SpeedControl>();
        }
    }
}
