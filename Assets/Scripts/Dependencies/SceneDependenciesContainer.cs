using UnityEngine;

namespace WinterGameJam
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private InputControll inputControll;
        [SerializeField] private SoundPlayer soundPlayer;
        [SerializeField] private MusicPlayer musicPlayer;

        private void Awake()
        {
            FindAllObjectToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<InputControll>(inputControll, monoBehaviourInScene);
            Bind<SoundPlayer>(soundPlayer, monoBehaviourInScene);
            Bind<MusicPlayer>(musicPlayer, monoBehaviourInScene);
        }
    }
}
