using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterGameJam
{
    public class GlobalDependenciesContainer : Dependency
    {
        //[SerializeField] private LevelList levelList;
        [SerializeField] private SettingsLoader settingsLoader;
        [SerializeField] private SoundPlayer soundPlayer;
        [SerializeField] private MusicPlayer musicPlayer;

        private static GlobalDependenciesContainer instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            FindAllObjectToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            //Bind<LevelList>(levelList, monoBehaviourInScene);
            Bind<SettingsLoader>(settingsLoader, monoBehaviourInScene);
            Bind<SoundPlayer>(soundPlayer, monoBehaviourInScene);
            Bind<MusicPlayer>(musicPlayer, monoBehaviourInScene);
        }
    }
}
