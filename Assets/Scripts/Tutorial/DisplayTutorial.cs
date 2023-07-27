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
    }
}
