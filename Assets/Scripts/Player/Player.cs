using System;
using UnityEngine;

namespace WinterGameJam
{
    public class Player : MonoBehaviour, IDependency<InputControll>
    {
        [Header("Movement")]
        [SerializeField] private float speed = 4;
        [SerializeField] private float xClamp = 4.5f;


        [SerializeField] private float jumpForce = 4;
        [SerializeField] private float fallForce = 4;
        [SerializeField] private float yClamp = 3.5f;

        [Header("Rotation")]
        //[SerializeField] private float xRotFactor = 5f;
        //[SerializeField] private float xMoveRot = 10f;
        [SerializeField] private float yRotFactor = 5f;
        [SerializeField] private float yMoveRot = 10f;

        private float xMove;
        private float yMove;
        private float jump;

        [Header("DEBUG")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool isFalling;

        private InputControll inputControll;
        public void Construct(InputControll obj) => inputControll = obj;

        private void Update()
        {
            GetAxis();
            HorizontalMovement();
            Rotate();
            VerticalMovement();
        }

        private void GetAxis()
        {
            xMove = inputControll.HorizontalAxis;
            yMove = inputControll.VerticalAxis;
            jump = inputControll.Jump;
        }

        private void HorizontalMovement()
        {
            float xOffset = xMove * speed * Time.deltaTime;
            float newXPos = transform.localPosition.x + xOffset;
            float clampXPos = Mathf.Clamp(newXPos, -xClamp, xClamp);

            /*float yOffset = yMove * jumpForce * Time.deltaTime;
            float newYPos = transform.localPosition.y + yOffset;
            float clampYPos = Mathf.Clamp(newYPos, -yClamp, yClamp);*/


            transform.localPosition = new Vector3(clampXPos, transform.localPosition.y, transform.localPosition.z);
                //clampYPos, transform.localPosition.z);
        }

        private void VerticalMovement()
        {
            if (isGrounded && (jump > 0))
            {
                isJumping = true;
            } 

            if (isJumping)
            {
                float yOffset = jumpForce * Time.deltaTime;
                float newYPos = transform.localPosition.y + yOffset;
                float clampYPos = Mathf.Clamp(newYPos, 0, yClamp);

                if (Math.Abs(clampYPos) >= yClamp)
                {
                    isJumping = false;
                    isFalling = true;
                }

                transform.localPosition = new Vector3(transform.localPosition.x, clampYPos, transform.localPosition.z);
            }

            if (isFalling)
            {
                float yOffset = -fallForce * Time.deltaTime;
                float newYPos = transform.localPosition.y + yOffset;

                if (isGrounded)
                    isFalling = false;

                transform.localPosition = new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
            }
        }

        private void Rotate()
        {
            //float xRot = transform.localPosition.y * xRotFactor + yMove * xMoveRot;
            float yRot = transform.localPosition.x * yRotFactor + xMove * yMoveRot;

            //transform.localRotation = Quaternion.Euler(xRot, 0, 0);
            transform.localRotation = Quaternion.Euler(0, yRot, 0);
        }


        private void OnCollisionEnter(Collision collision)
        {
            IsGroundedUpdate(collision, true);
        }

        private void OnCollisionExit(Collision collision)
        {
            IsGroundedUpdate(collision, false);
        }

        private void IsGroundedUpdate(Collision collision, bool value)
        {
            if (collision.gameObject.tag == ("Ground"))
            {
                isGrounded = value;
            }
        }
    }
}