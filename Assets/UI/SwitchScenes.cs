using Assets.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.UI
{
    class SwitchScenes : MonoBehaviour
    {
        [SerializeField] private Scenes _scene;

        private void OnMouseDown()
        {
            SceneManager.LoadScene((int)_scene);
        }
    }
}
