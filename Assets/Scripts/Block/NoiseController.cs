using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block
{
    class NoiseController : MonoBehaviour // TODO: добавить звук ???
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private HandleRotate _volume;

        private void OnEnable()
        {
            _volume.AddListener(VolumeChangedHandler);
        }

        private void OnDisable()
        {
            _volume.RemoveListener(VolumeChangedHandler);
        }

        private void VolumeChangedHandler()
        {
            _audioSource.volume = _volume.Value;
        }
    }
}
