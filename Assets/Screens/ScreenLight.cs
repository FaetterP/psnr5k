using Assets.Switches;
using UnityEngine;

namespace Assets.Screens
{
    [RequireComponent(typeof(Light))]
    class ScreenLight : MonoBehaviour
    {
        [SerializeField] private Lever _lever;
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
            _lever.AddListener(ChangeLight);
        }

        private void OnDisable()
        {
            _lever.RemoveListener(ChangeLight);
        }

        private void ChangeLight(bool value)
        {
            _thisLight.enabled = value;
        }

    }
}
