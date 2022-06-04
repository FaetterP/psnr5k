using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Utilities
{
    class Loading : MonoBehaviour
    {
        [SerializeField] private Text _loadingText;
        [SerializeField] private Canvas _loadingCanvas;
        private AsyncOperation _loadCoroutine;

        public void Load(Scenes scene)
        {
            StartCoroutine(LoadScene((int)scene));
        }

        IEnumerator LoadScene(int num)
        {
            //yield return new WaitForSeconds(1f);
            _loadingCanvas.gameObject.SetActive(true);
            _loadCoroutine = SceneManager.LoadSceneAsync(num);
            while (_loadCoroutine.isDone == false)
            {
                int progress = (int)(_loadCoroutine.progress * 100);
                _loadingText.text = progress + "%";
                yield return 0;
            }
        }
    }
}
