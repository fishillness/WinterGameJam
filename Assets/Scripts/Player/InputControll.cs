using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class InputControll : MonoBehaviour
    {
        public static event UnityAction OnPressedPause;

        private float verticalAxis;
        private float horizontalAxis;

        public float VerticalAxis => verticalAxis;
        public float HorizontalAxis => horizontalAxis;

        private void Update()
        {
            UpdateAxis();
            CheckKeyDowm();
        }
        
        private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
        }
        private void CheckKeyDowm()
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                OnPressedPause?.Invoke();
            }
        }
    }
}
