using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterGameJam
{
    public static class SceneLoader
    {
        public const string MainMenuSceneTitle = "Main Menu";

        public static void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneTitle);
        }

        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        public static string GetActiveScene()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
