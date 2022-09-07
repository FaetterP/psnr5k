using Assets.Utilities;
using UnityEngine;

namespace Assets.UI
{
    class SwitchScenes : MonoBehaviour
    {
        [SerializeField] private Scenes _scene;
        private Loading _loading;

        private void Awake()
        {
            _loading = FindObjectOfType<Loading>();
        }

        private void OnMouseDown()
        {
            _loading.Load(_scene);
        }
    }
}
