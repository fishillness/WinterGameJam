using UnityEngine;

namespace WinterGameJam
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject instructionPanel;
        //[SerializeField] private UIInstructionPanel instructionPanel;

        private Pauser pauser;

        public void Construct(Pauser obj) => pauser = obj;

        private void Start()
        {
            pausePanel.SetActive(false);
            instructionPanel.SetActive(false);
            Pauser.PauseStateChange += ChangePausePanelActive;
        }

        private void OnDestroy()
        {
            Pauser.PauseStateChange -= ChangePausePanelActive;
        }

        private void ChangePausePanelActive(bool isPause)
        {
            if (isPause)
                pausePanel.SetActive(true);
            else
                pausePanel.SetActive(false);
        }

        public void ContinueButton()
        {
            pauser.UnPause();
            pausePanel.SetActive(false);
        }

        public void ReplayButton()
        {
            pauser.UnPause();
            SceneLoader.Restart();
        }

        public void InstractionButton()
        {
            pausePanel.SetActive(false);
            instructionPanel.SetActive(true);
        }

        public void MenuButton()
        {
            pauser.UnPause();
            SceneLoader.LoadMainMenu();
        }

        public void OkButton()
        {
            pausePanel.SetActive(true);
            instructionPanel.SetActive(false);
        }

    }
}
