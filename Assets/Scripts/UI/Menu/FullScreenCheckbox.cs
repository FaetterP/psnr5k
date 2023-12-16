using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    [RequireComponent(typeof(Toggle))]
    public class FullScreenCheckbox : MonoBehaviour
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.isOn = Screen.fullScreen;
            _toggle.onValueChanged.AddListener(UpdateQuality);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(UpdateQuality);
        }

        private void UpdateQuality(bool value)
        {
            Screen.fullScreen = value;
            PlayerPrefs.SetInt(Constants.FullScreen, value ? 1 : 0);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void SetStartValue()
        {
            bool value = PlayerPrefs.GetInt(Constants.FullScreen) == 1;
            Screen.fullScreen = value;
        }
    }
}
