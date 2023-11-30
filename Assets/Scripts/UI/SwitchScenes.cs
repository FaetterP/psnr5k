using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class SwitchScenes : MonoBehaviour
    {
        [SerializeField] private Scenes _scene;

        private void OnMouseUp()
        {
            Loading.Instance.Load(_scene);
        }
    }
}
