using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.UI.Research
{
    [RequireComponent(typeof(Animator))]
    class ObjectWithMessage : MonoBehaviour
    {
        [SerializeField] private ObjectGroup _group;
        [Header("Info for message screen")]
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private string _message;

        private Animator _thisAnimator;
        private ReadString _string;

        private void Awake()
        {
            _thisAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            DisableAnimator();
        }

        private void OnMouseDown()
        {
            MessageScreen.Instance.Show(_sprite, _name, _message);

            EnableAnimator();
            _group.ActivateDetail(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // if (_messageScreen != null)
                // {
                //     _messageScreen.gameObject.SetActive(false);
                // }
                DisableAnimator();
            }
        }

        public void Deactivate()
        {
            DisableAnimator();
        }

        private void DisableAnimator()
        {
            _thisAnimator.Play("Twinkle", 0, 0);
            _thisAnimator.speed = 0;
            //_thisAnimator.enabled = false;
        }

        private void EnableAnimator()
        {
            _thisAnimator.speed = 1;
            //_thisAnimator.enabled = true;
            _thisAnimator.Play("Twinkle", 0, 0);
        }
    }
}
