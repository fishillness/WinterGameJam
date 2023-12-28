using UnityEngine;
using UnityEngine.UI;

namespace WinterGameJam
{
    public class UIEndGamePanel : MonoBehaviour, IDependency<Pauser>, IDependency<LevelController>, IDependency<BoxCounter>
    {
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private GameObject nextButton;

        [Header("Texts")]
        [SerializeField] private Text infoAboutLevel;
        [SerializeField] private Text boxCountText;
        [SerializeField] private Text timeCountText;

        [Header("Sounds")]
        [SerializeField] private bool PlaySoundWin;
        [SerializeField] private SoundType winSound;
        [SerializeField] private bool PlaySoundLose;
        [SerializeField] private SoundType loseSound;

        private string winText = "Вы догнали Санту!";
        private string loseText = "Вы упустили санту…"; 

        private Pauser pauser;
        public void Construct(Pauser obj) => pauser = obj;
        private LevelController levelController;
        public void Construct(LevelController obj) => levelController = obj;
        private BoxCounter boxCounter;
        public void Construct(BoxCounter obj) => boxCounter = obj;

        private void Start()
        {
            endGamePanel.SetActive(false);

            /*
            if (SceneLoader.IsLastLevel(levelList))
                nextButton.SetActive(false);

            */

            //LevelController.OnCompletedLevel += OpenEndGamePanel;
            Santa.OnCaughtSanta += OpenWinGamePanel;
            SantaClaus.OnCaughtSanta += OpenWinGamePanel;
            LevelController.TimerIsOver += OpenLoseGamePanel;
        }

        private void Update()
        {

        }

        private void OnDestroy()
        {
            //LevelController.OnCompletedLevel -= OpenEndGamePanel;
            Santa.OnCaughtSanta -= OpenWinGamePanel;
            SantaClaus.OnCaughtSanta -= OpenWinGamePanel;
            LevelController.TimerIsOver -= OpenLoseGamePanel;
        }

        private void OpenWinGamePanel()
        {
            UpdateTexts();
            endGamePanel.SetActive(true);
            infoAboutLevel.text = winText;
            pauser.Pause();

            if (PlaySoundWin)
                SoundPlayer.Instance.Play(winSound);
                //SoundPlayer.instance.Play(winSound);
        }

        private void OpenLoseGamePanel()
        {
            UpdateTexts();
            endGamePanel.SetActive(true);
            infoAboutLevel.text = loseText;
            pauser.Pause();

            if (PlaySoundLose)
                SoundPlayer.Instance.Play(SoundType.Lose);
                //SoundPlayer.instance.Play(loseSound);
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

        private void UpdateTexts()
        {
            boxCountText.text = boxCounter.CurrentBoxCount.ToString();
            timeCountText.text = ConvertToMinutes(levelController.SpentTime());
        }

        private string ConvertToMinutes(float currentTime)
        {
            if (currentTime < 0)
                currentTime = 0;

            float minutes = Mathf.Floor(currentTime / 60);
            float sec = currentTime - minutes * 60;

            return (minutes + ":" + Mathf.Floor(sec));
        }
    }
}