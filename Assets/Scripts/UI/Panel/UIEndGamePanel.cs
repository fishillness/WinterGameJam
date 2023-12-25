using UnityEngine;

namespace WinterGameJam
{
    public class UIEndGamePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private GameObject nextButton;

        private Pauser pauser;
        public void Construct(Pauser obj) => pauser = obj;

        private void Start()
        {
            endGamePanel.SetActive(false);

            /*
            if (SceneLoader.IsLastLevel(levelList))
                nextButton.SetActive(false);

            */

            //LevelController.OnCompletedLevel += OpenEndGamePanel;
            Santa.OnCaughtSanta += OpenEndGamePanel;
        }

        private void OnDestroy()
        {
            //LevelController.OnCompletedLevel -= OpenEndGamePanel;
            Santa.OnCaughtSanta -= OpenEndGamePanel;
        }

        private void OpenEndGamePanel()
        {
            endGamePanel.SetActive(true);
            pauser.Pause();
        }

        public void NextButton()
        {
            Debug.Log("There are no levels list yet.");
            //SceneLoader.LoadNexLevel(levelList);
            pauser.UnPause();
        }

        public void ReplayButton()
        {
            SceneLoader.Restart();
            pauser.UnPause();
        }

        public void MenuButton()
        {
            SceneLoader.LoadMainMenu();
            pauser.UnPause();
        }
    }
}