using UnityEngine;

namespace WinterGameJam
{
    public class UIInteractPanel : MonoBehaviour
    {
        [SerializeField] private GameObject interactPanel;

        private void Start()
        {
            CloseInteractPanel();

            Santa.OnPlayerEnterSanta += OpenInteractPanel;
            Santa.OnPlayerExitSanta += CloseInteractPanel;
        }

        private void OnDestroy()
        {
            Santa.OnPlayerEnterSanta -= OpenInteractPanel;
            Santa.OnPlayerExitSanta -= CloseInteractPanel;
        }

        public void OpenInteractPanel()
        {
            interactPanel.SetActive(true);
        }

        public void CloseInteractPanel()
        {
            interactPanel.SetActive(false);
        }
    }
}
