using System;
using System.Collections.ObjectModel;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    [RequireComponent(typeof(Dropdown))]
    public class QualityDropdown : MonoBehaviour
    {
        private Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<Dropdown>();
        }

        private void OnEnable()
        {
            _dropdown.value = QualitySettings.GetQualityLevel();
            _dropdown.onValueChanged.AddListener(UpdateQuality);
        }

        private void OnDisable()
        {
            _dropdown.onValueChanged.RemoveListener(UpdateQuality);
        }

        private void UpdateQuality(int value)
        {
            QualitySettings.SetQualityLevel(value);
            PlayerPrefs.SetInt(Constants.Quality, value);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void SetStartQuality()
        {
            int value = PlayerPrefs.GetInt(Constants.Quality);
            QualitySettings.SetQualityLevel(value);
        }
    }
}