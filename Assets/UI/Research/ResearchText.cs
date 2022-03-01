using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.Research
{
    [RequireComponent(typeof(Text))]
    class ResearchText : MonoBehaviour
    {
        private Text _thisText;

        private void Awake()
        {
            _thisText = GetComponent<Text>();    
        }

        public void SetText(string message)
        {
            _thisText.text = message;
        }
    }
}
