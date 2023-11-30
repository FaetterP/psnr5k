using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.UI.Research
{
    class MessageScreen : MonoBehaviour
    {
        private static MessageScreen s_instance;

        [SerializeField] private Image _image;
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;

        public static MessageScreen Instance => s_instance;

        private void Awake()
        {
            s_instance = this;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Show(Sprite sprite, string key)
        {
            gameObject.SetActive(true);

            _image.sprite = sprite;
            _name.text = Messages.getValue($"{key}.name");
            _description.text = Messages.getValue($"{key}.desc");
        }
    }
}
