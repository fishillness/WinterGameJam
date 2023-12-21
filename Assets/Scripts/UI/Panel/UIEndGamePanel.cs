using UnityEngine;

namespace WinterGameJam
{
    public class UIEndGamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private GameObject nextButton;


        private void Start()
        {
            endGamePanel.SetActive(false);

            /*
            if (SceneLoader.IsLastLevel(levelList))
                nextButton.SetActive(false);

            */

            //LevelController.OnCompletedLevel += OpenEndGamePanel;
        }

        private void OnDestroy()
        {
            //LevelController.OnCompletedLevel -= OpenEndGamePanel;
        }

        private void OpenEndGamePanel()
        {
            endGamePanel.SetActive(true);
        }

        public void NextButton()
        {
            Debug.Log("There are no levels list yet.");
            //SceneLoader.LoadNexLevel(levelList);
        }

        public void ReplayButton()
        {
            SceneLoader.Restart();
        }

        public void MenuButton()
        {
            SceneLoader.LoadMainMenu();
        }
    }
}