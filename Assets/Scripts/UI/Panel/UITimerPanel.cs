using UnityEngine;
using UnityEngine.UI;

namespace WinterGameJam
{
    public class UITimerPanel : MonoBehaviour, IDependency<LevelController>
    {
        [SerializeField] private Text timeText;

        private LevelController levelController;
        public void Construct(LevelController obj) => levelController = obj;


        private void Update()
        {

            timeText.text = levelController.CurrentTime.ToString();
        }


    }
}
