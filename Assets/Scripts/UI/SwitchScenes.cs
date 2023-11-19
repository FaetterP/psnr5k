using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class SwitchScenes : MonoBehaviour
    {
        [SerializeField] private Scenes _scene;
        private Loading _loading;

        private void Awake()
        {
            _loading = FindObjectOfType<Loading>();
        }

        private void OnMouseUp()
        {
            _loading.Load(_scene);
        }
    }
}
