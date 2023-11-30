using Assets.Scripts.UI.Research;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Research
{
    public class Tube : MonoBehaviour
    {
        private UnityEvent e_onClicked = new UnityEvent();

        private void OnMouseDown()
        {
            e_onClicked.Invoke();
        }

        public void AddListenerOnClicked(UnityAction action)
        {
            e_onClicked.AddListener(action);
        }

        public void RemoveListenerOnClicked(UnityAction action)
        {
            e_onClicked.RemoveListener(action);
        }
    }
}
