using UnityEngine;

namespace WinterGameJam
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private Pauser pauser;
        [SerializeField] private Player player;
        [SerializeField] private InputControll inputControll;
        [SerializeField] private SpeedControl speedControl;
        [SerializeField] private RoadGenerator roadGenerator;
        [SerializeField] private LevelController levelController;

        private void Awake()
        {
            FindAllObjectToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Pauser>(pauser, monoBehaviourInScene);
            Bind<Player>(player, monoBehaviourInScene);
            Bind<InputControll>(inputControll, monoBehaviourInScene);
            Bind<SpeedControl>(speedControl, monoBehaviourInScene);
            Bind<RoadGenerator>(roadGenerator, monoBehaviourInScene);
            Bind<LevelController>(levelController, monoBehaviourInScene);
        }
    }
}
