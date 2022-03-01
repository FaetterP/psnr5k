using UnityEngine;

namespace Assets.UI.Research
{
    class ObjectWithMessage : MonoBehaviour
    {
        [SerializeField] private ResearchText _text;
        [SerializeField] [TextArea] private string _message;

        private void OnMouseDown()
        {
            _text.SetText(_message);
        }
    }
}
