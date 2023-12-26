using UnityEngine;

namespace WinterGameJam
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private GameObject timerPanel;
        [SerializeField] private GameObject boxPanel;
        [SerializeField] private GameObject speedPanel;

        private void Start()
        {
            SetPanelsActive(true);
            Pauser.PauseStateChange += OnPauseStateChange;
        }

        private void OnDestroy()
        {
            Pauser.PauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool state)
        {
            SetPanelsActive(!state);
        }

        private void SetPanelsActive(bool state)
        {
            timerPanel.SetActive(state);
            boxPanel.SetActive(state);
            speedPanel.SetActive(state);
        }
    }
}
