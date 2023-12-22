using UnityEngine;
using UnityEngine.Events;

namespace WinterGameJam
{
    public class InputControll : MonoBehaviour
    {
        public static event UnityAction OnPressedPause;

        private float verticalAxis;
        private float horizontalAxis;
        private float jump;

        public float VerticalAxis => verticalAxis;
        public float HorizontalAxis => horizontalAxis;
        public float Jump => jump;

        private void Update()
        {
            UpdateAxis();
            CheckKeyDowm();
        }
        
        private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            jump = Input.GetAxis("Jump");
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
