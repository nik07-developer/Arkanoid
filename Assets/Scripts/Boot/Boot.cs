using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Boot
{
    public class Boot : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}