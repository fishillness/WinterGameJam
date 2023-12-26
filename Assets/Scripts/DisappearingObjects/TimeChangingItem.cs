using UnityEngine;

namespace WinterGameJam
{
    public class TimeChangingItem : DisappearingObject
    {
        [SerializeField] private float second;

        private LevelController levelController;

        protected override void OnPlayerEnter()
        {
            levelController.AddTime(second);
            base.OnPlayerEnter();
        }

        public void SetLevelController(LevelController controller)
        {
            levelController = controller;
        }
    }
}
