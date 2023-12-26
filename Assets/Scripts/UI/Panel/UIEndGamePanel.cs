using UnityEngine;
using UnityEngine.UI;

namespace WinterGameJam
{
    public class UIEndGamePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private Text infoAboutLevel; 
        [SerializeField] private GameObject nextButton;

        private string winText = "Level complete!";
        private string loseText = "Game jver!"; 

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
            Santa.OnCaughtSanta += OpenWinGamePanel;
            LevelController.TimerIsOver += OpenLoseGamePanel;
        }

        private void OnDestroy()
        {
            //LevelController.OnCompletedLevel -= OpenEndGamePanel;
            Santa.OnCaughtSanta -= OpenWinGamePanel;
            LevelController.TimerIsOver -= OpenLoseGamePanel;
        }

        private void OpenWinGamePanel()
        {
            endGamePanel.SetActive(true);
            infoAboutLevel.text = winText;
            pauser.Pause();
        }

        private void OpenLoseGamePanel()
        {
            endGamePanel.SetActive(true);
            infoAboutLevel.text = loseText;
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