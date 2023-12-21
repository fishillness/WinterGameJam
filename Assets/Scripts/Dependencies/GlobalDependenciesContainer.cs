using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterGameJam
{
    public class GlobalDependenciesContainer : Dependency
    {
        //[SerializeField] private LevelList levelList;
        [SerializeField] private Pauser pauser;
        //[SerializeField] private SettingsLoader settingsLoader;

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
            Bind<Pauser>(pauser, monoBehaviourInScene);
            //Bind<SettingsLoader>(settingsLoader, monoBehaviourInScene);
        }
    }
}
