using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    [RequireComponent(typeof(Light))]
    class ScreenLight : MonoBehaviour
    {
        [SerializeField] private Lever _work;
        private Light _thisLight;

        private void Awake()
        {
            _thisLight = GetComponent<Light>();
        }

        private void Start()
        {
            _thisLight.enabled = false;
        }

        private void OnEnable()
        {
            _work.AddListener(WorkChangedHandler);
        }

        private void OnDisable()
        {
            _work.RemoveListener(WorkChangedHandler);
        }

        private void WorkChangedHandler()
        {
            _thisLight.enabled = _work.Value;
        }

    }
}
