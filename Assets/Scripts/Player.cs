using UnityEngine;

namespace WinterGameJam
{
    public class Player : MonoBehaviour, IDependency<InputControll>
    {
        [Header("Movement")]
        [SerializeField] private float speed = 4;
        [SerializeField] private float xClamp = 4.5f;
        [SerializeField] private float jumpForce = 4;
        [SerializeField] private float yClamp = 3.5f;

        [Header("Rotation")]
        //[SerializeField] private float xRotFactor = 5f;
        //[SerializeField] private float xMoveRot = 10f;
        [SerializeField] private float yRotFactor = 5f;
        [SerializeField] private float yMoveRot = 10f;

        private float xMove;
        private float yMove;

        private InputControll inputControll;
        public void Construct(InputControll obj) => inputControll = obj;

        private void Update()
        {
            GetAxis();
            Move();
            Rotate();
        }

        private void GetAxis()
        {
            xMove = inputControll.HorizontalAxis;
            yMove = inputControll.VerticalAxis;
        }

        private void Move()
        {
            float xOffset = xMove * speed * Time.deltaTime;
            float newXPos = transform.localPosition.x + xOffset;
            float clampXPos = Mathf.Clamp(newXPos, -xClamp, xClamp);

            float yOffset = yMove * jumpForce * Time.deltaTime;
            float newYPos = transform.localPosition.y + yOffset;
            float clampYPos = Mathf.Clamp(newYPos, -yClamp, yClamp);


            transform.localPosition = new Vector3(clampXPos,
                clampYPos, transform.localPosition.z);
        }

        private void Rotate()
        {
            //float xRot = transform.localPosition.y * xRotFactor + yMove * xMoveRot;
            float yRot = transform.localPosition.x * yRotFactor + xMove * yMoveRot;

            transform.localRotation = Quaternion.Euler(0, yRot, 0);
        }
    }
}