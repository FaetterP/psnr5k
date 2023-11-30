using Assets.Scripts.UI.Research;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Research
{
    public class BigObject : MonoBehaviour
    {
        [SerializeField] private GameObject _fullsizeModel;
        [SerializeField] private string _key;
        [SerializeField] private Sprite _sprite;

        private UnityEvent e_onClicked = new UnityEvent();

        private void OnMouseDown()
        {
            MessageScreen.Instance.Show(_sprite, _key);
            BigObjectsSpawn.Instance.SpawnModel(_fullsizeModel);
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
