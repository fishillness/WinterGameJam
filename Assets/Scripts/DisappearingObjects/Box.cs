using UnityEngine;

namespace WinterGameJam
{
    public class Box : DisappearingObject
    {
        [SerializeField] private int countCount = 1;

        private BoxCounter boxCounter;

        protected override void OnPlayerEnter()
        {
            boxCounter.AddBox(countCount);
            base.OnPlayerEnter();
        }

        public void SetBoxCounter(BoxCounter counter)
        {
            boxCounter = counter;
        }
    }
}
