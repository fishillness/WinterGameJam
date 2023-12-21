using UnityEngine;

namespace WinterGameJam
{
    public class UIPausePanel : MonoBehaviour//, IDependency<Pauser>
    {
        [SerializeField] private GameObject pausePanel;
        //[SerializeField] private UIInstructionPanel instructionPanel;
        [SerializeField] private Pauser pauser;

        public void Construct(Pauser obj) => pauser = obj;

        private void Start()
        {
            pausePanel.SetActive(false);
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
            Debug.Log("The instruction bar has not yet been added.");
            //instructionPanel.OpenPanel();
        }

        public void MenuButton()
        {
            pauser.UnPause();
            SceneLoader.LoadMainMenu();
        }
    }
}
