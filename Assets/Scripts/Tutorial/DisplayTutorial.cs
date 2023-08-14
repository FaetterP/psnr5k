using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tutorial
{
    class DisplayTutorial : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void ShowMessage(string message)
        {
            _text.text = message;
        }

        public void FinishTutorial()
        {
            _text.text = "Завершено";
            StartCoroutine(Close());
        }

        private IEnumerator Close()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}
