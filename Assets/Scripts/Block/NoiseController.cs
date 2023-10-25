using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block
{
    class NoiseController : MonoBehaviour // TODO: добавить звук ???
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private HandleRotate _volumeHandle;

        private void OnEnable()
        {
            _volumeHandle.AddListener(VolumeChangedHandler);
        }

        private void OnDisable()
        {
            _volumeHandle.RemoveListener(VolumeChangedHandler);
        }

        private void VolumeChangedHandler(float value)
        {
            _audioSource.volume = value / 100f;
        }
    }
}
