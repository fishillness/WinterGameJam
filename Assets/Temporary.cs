using UnityEngine;

namespace WinterGameJam
{
    public class Temporary : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void OpenScene()
        {
            SceneLoader.LoadScene(sceneName);
        }
    }
}
