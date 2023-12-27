using System;
using UnityEngine;

namespace WinterGameJam
{
    public class Player : MonoBehaviour, IDependency<InputControll>
    {
        [Header("Movement")]
        [SerializeField] private float speed = 4;
        [SerializeField] private float xClamp = 4.5f;

        [Header("Jump")]
        [SerializeField] private float jumpForce = 4;
        [SerializeField] private float fallForce = 4;
        [SerializeField] private float yClamp = 3.5f;
        //[SerializeField] private float timeInJump;

        [Header("Rotation")]
        [SerializeField] private float yRotFactor = 5f;
        [SerializeField] private float yMoveRot = 10f;
        [SerializeField] private float degreessJumpClamp = 30;

        private float xMove;
        //private float jump;
        private float groundYPos;
        private bool controlAvailable;

        [Header("DEBUG")]
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool isFalling;

        private InputControll inputControll;
        public void Construct(InputControll obj) => inputControll = obj;

        private void Start()
        {
            SetControlAvailable();
        }

        private void Update()
        {
            if (controlAvailable)
            {
                GetAxis();
                HorizontalMovement();
                VerticalMovement();
                Rotate();
            }
        }

        private void GetAxis()
        {
            xMove = inputControll.HorizontalAxis;
            //jump = inputControll.Jump;
        }

        private void HorizontalMovement()
        {
            float xOffset = xMove * speed * Time.deltaTime;
            float newXPos = transform.localPosition.x + xOffset;
            float clampXPos = Mathf.Clamp(newXPos, -xClamp, xClamp);

            transform.localPosition = new Vector3(clampXPos, transform.localPosition.y, transform.localPosition.z);
        }

        private void VerticalMovement()
        {
            if (isJumping)
            {
                float yOffset = jumpForce * Time.deltaTime;
                float newYPos = transform.localPosition.y + yOffset;
                float clampYPos = Mathf.Clamp(newYPos, 0, yClamp + groundYPos);

                if (Math.Abs(newYPos) >= (yClamp + groundYPos))
                {
                    isJumping = false;
                    isFalling = true;
                    //StartCoroutine(StayInAir());
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

            /*if (isGrounded && (jump > 0) && !isJumping)
            {
                isJumping = true;
                groundYPos = transform.localPosition.y;
            }*/
        }

        private void Rotate()
        {
            float yRot = transform.localPosition.x * yRotFactor + xMove * yMoveRot;
            float xRot = 0;

            if (isJumping || isFalling)
            {
                xRot = -degreessJumpClamp * transform.localPosition.y / (yClamp + groundYPos);
                xRot = Mathf.Clamp(xRot, -degreessJumpClamp, 0);
            } 
            transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
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

        public void SetControlAvailable()
        {
            controlAvailable = true;
        }

        public void SetControlUnavailable()
        {
            controlAvailable = false;
        }

        /*private IEnumerator StayInAir()
        {
            yield return new WaitForSeconds(timeInJump);
            ChangeVariables();
        }

        private void ChangeVariables()
        {
            isJumping = false;
            isFalling = true;
        }*/
    }
}