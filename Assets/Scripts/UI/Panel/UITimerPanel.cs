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

            timeText.text = ConvertToMinutes(levelController.CurrentTime);
        }

        private string ConvertToMinutes(float currentTime)
        {
            float minutes = Mathf.Floor(currentTime / 60);
            float sec = currentTime - minutes * 60;

            return (minutes + ":" + Mathf.Floor(sec));
        }
    }
}
