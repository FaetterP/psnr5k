using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block
{
    class NoiseController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private HandleRotate _volumeHandle;

        private int _volume;

        private void OnEnable()
        {
            _volumeHandle.AddListener(SetVolumeValue);
        }

        private void OnDisable()
        {
            _volumeHandle.RemoveListener(SetVolumeValue);
        }

        private void SetVolumeValue(int value)
        {
            _volume = value;
            UpdateValues();
        }

        private void UpdateValues()
        {
            _audioSource.volume = _volume / 100f;
        }
    }
}
