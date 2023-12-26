using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class BoxCounter : MonoBehaviour
    {
        public static event UnityAction<int> OnBoxCountUpdate;

        private int currentBoxCount;

        public int CurrentBoxCount => currentBoxCount;

        public void AddBox(int value)
        {
            currentBoxCount += value;
            OnBoxCountUpdate?.Invoke(currentBoxCount);
        }
    }
}
