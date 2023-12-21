using UnityEngine;

namespace WinterGameJam
{
    public class UIMainMenu : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private GameObject continueButton;

        [Header("Panels")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject levelsPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject controlPanel;
        [SerializeField] private GameObject confirmationPanel;

        private bool isThereSaves;

        private void Start()
        {
            OpenPanel(menuPanel);
            confirmationPanel.SetActive(false);

            //TO DO: проверка на налиие сохранений
            isThereSaves = true; //временно
            //isThereSaves = (LevelUtil.FindSavedByLevel("Level 1") != 0);
            continueButton.SetActive(isThereSaves);
        }

        public void MenuButton()
        {
            OpenPanel(menuPanel);
        }

        public void NewGameButton()
        {
            if (isThereSaves)
            {
                confirmationPanel.SetActive(true);
            }
            else
            {
                OpenPanel(levelsPanel);
            }
        }

        public void OkButton()
        {
            //TO DO: удаление сохранений
            confirmationPanel.SetActive(false);
            OpenPanel(levelsPanel);
        }
        public void CancelButton()
        {
            confirmationPanel.SetActive(false);
        }

        public void ContinueButton()
        {
            OpenPanel(levelsPanel);
        }

        public void SettingsButton()
        {
            OpenPanel(settingsPanel);
        }

        public void ControlButton()
        {
            OpenPanel(controlPanel);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        private void OpenPanel(GameObject panel)
        {
            menuPanel.SetActive(false);
            levelsPanel.SetActive(false);
            settingsPanel.SetActive(false);
            controlPanel.SetActive(false);

            panel.SetActive(true);
        }
    }
}