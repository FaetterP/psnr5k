using Assets.Utilities;
using UnityEngine;

namespace Assets.UI.Research
{
    class ObjectWithMessage : MonoBehaviour
    {
        [SerializeField] private ResearchText _researchText;
        [SerializeField] private string _key;

        private ReadString _string;

        private void Awake()
        {
            _string = new ReadString(_key);
        }

        private void OnMouseDown()
        {
            _researchText.SetText(_string.GetValue());
        }
    }
}
