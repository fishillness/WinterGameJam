using UnityEngine;

namespace WinterGameJam
{
    public class TEST_levelButton : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void OpenScene()
        {
            SceneLoader.LoadScene(sceneName);
        }
    }
}
