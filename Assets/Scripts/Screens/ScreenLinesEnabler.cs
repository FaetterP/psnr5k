using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    class ScreenLinesEnabler : MonoBehaviour
    {
        [SerializeField] private Lever _lever;

        private void OnEnable()
        {
            _lever.AddListener(ChangeLight);
        }

        private void ChangeLight(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
