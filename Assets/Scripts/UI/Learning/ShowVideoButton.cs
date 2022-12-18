using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.UI.Learning
{
    class ShowVideoButton : MonoBehaviour
    {
        [SerializeField] private VideoClip _clip;
        [SerializeField] private RawImage _videoPlayer;

        private void OnMouseDown()
        {
            Debug.Log(123);
            if (_videoPlayer.enabled)
            {
                _videoPlayer.enabled = false;
                return;
            }
            else
            {
                _videoPlayer.enabled = true;
                return;
            }
        }
    }
}
