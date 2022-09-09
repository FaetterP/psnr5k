using Assets.Utilities;
using UnityEngine;

namespace Assets.UI.Research
{
    [RequireComponent(typeof(Animator))]
    class ObjectWithMessage : MonoBehaviour
    {
        [SerializeField] private ResearchText _researchText;
        [SerializeField] private string _key;
        [SerializeField] private ObjectGroup _group;
        [SerializeField] private MessageScreen _messageScreen;

        private Animator _thisAnimator;
        private ReadString _string;

        private void Awake()
        {
            _string = new ReadString(_key);
            _thisAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            DisableAnimator();
        }

        private void OnMouseDown()
        {
            _messageScreen.gameObject.SetActive(true);
            _researchText.SetText(_string.GetValue());

            EnableAnimator();
            _group.ActivateDetail(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_messageScreen!=null)
                {
                    _messageScreen.gameObject.SetActive(false);
                }
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
