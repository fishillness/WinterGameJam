using UnityEngine;

namespace WinterGameJam
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float xSpeed = 4;
        [SerializeField] private float xClamp = 4.5f;
        [SerializeField] private float ySpeed = 4;
        [SerializeField] private float yClamp = 3.5f;

        private float xMove;
        private float yMove;

        private void Start()
        {
            
        }

        private void Update()
        {
            //временно
            xMove = Input.GetAxis("Horizontal");
            yMove = Input.GetAxis("Vertical");
            //

            float xOffset = xMove * xSpeed * Time.deltaTime;
            float newXPos = transform.localPosition.x + xOffset;
            float clampXPos = Mathf.Clamp(newXPos, -xClamp, xClamp);

            float yOffset = yMove * ySpeed * Time.deltaTime;
            float newYPos = transform.localPosition.y + yOffset;
            float clampYPos = Mathf.Clamp(newYPos, -yClamp, yClamp);


            transform.localPosition = new Vector3(clampXPos,
                clampYPos, transform.localPosition.z);
        }
    }
}