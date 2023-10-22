using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    class Loading : MonoBehaviour
    {
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private Text _loadingText;
        [SerializeField] private Slider _loadingSlider;
        private AsyncOperation _loadCoroutine;

        public void Load(Scenes scene)
        {
            StartCoroutine(LoadScene((int)scene));
        }

        private IEnumerator LoadScene(int num)
        {
            _loadingCanvas.gameObject.SetActive(true);
            _loadCoroutine = SceneManager.LoadSceneAsync(num);
            while (_loadCoroutine.isDone == false)
            {
                _loadingSlider.value = _loadCoroutine.progress;
                int progress = (int)(_loadCoroutine.progress * 100);
                _loadingText.text = progress + "%";
                yield return 0;
            }
        }
    }
}
