using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.UI.Research
{
    class MessageScreen : MonoBehaviour
    {
        private static MessageScreen s_instance;

        [SerializeField] private Image _mask;
        [SerializeField] private Image _image;
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;

        private float widthUI;
        private float heightUI;
        float factorUI;

        public static MessageScreen Instance => s_instance;

        private void Awake()
        {
            s_instance = this;
            widthUI = _mask.rectTransform.rect.width;
            heightUI = _mask.rectTransform.rect.height;
            factorUI = widthUI / heightUI;
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

            float widthSprite = sprite.rect.width;
            float heighSprite = sprite.rect.height;
            float factorSprite = widthSprite / heighSprite;

            float newWidth = (factorSprite > factorUI) ? widthUI : (factorSprite * widthUI / factorUI);
            float newHeight = (factorSprite > factorUI) ? (heightUI * factorUI / factorSprite) : heightUI;

            _image.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
            _mask.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }
    }
}
