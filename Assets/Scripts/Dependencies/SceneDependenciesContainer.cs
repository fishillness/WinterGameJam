using UnityEngine;

namespace WinterGameJam
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private Pauser pauser;
        [SerializeField] private InputControll inputControll;
        [SerializeField] private SpeedControl speedControl;

        private void Awake()
        {
            FindAllObjectToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Pauser>(pauser, monoBehaviourInScene);
            Bind<InputControll>(inputControll, monoBehaviourInScene);
            Bind<SpeedControl>(speedControl, monoBehaviourInScene);
        }
    }
}
