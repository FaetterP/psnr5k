using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utilities;

namespace Assets.Scripts.UI.Research
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    class ObjectWithMessage : MonoBehaviour
    {
        [SerializeField] private ObjectWithMessage[] _group;
        [Header("Info for message screen")]
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _key;

        private Animator _thisAnimator;

        private static List<ObjectWithMessage> s_instances = new List<ObjectWithMessage>();

        private static void AdjustAnimations(ObjectWithMessage activated)
        {
            foreach (ObjectWithMessage instance in s_instances)
            {
                instance.DisableAnimator();
                foreach (ObjectWithMessage groupItem in activated._group)
                {
                    groupItem.DisableAnimator();
                }
            }

            activated.EnableAnimator();
            foreach (ObjectWithMessage groupItem in activated._group)
            {
                groupItem.EnableAnimator();
            }
        }

        private void Awake()
        {
            _thisAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            DisableAnimator();
        }

        private void OnEnable()
        {
            s_instances.Add(this);
        }

        private void OnDisable()
        {
            s_instances.Remove(this);
        }

        private void OnMouseDown()
        {
            AdjustAnimations(this);
            MessageScreen.Instance.Show(_sprite, _key);
        }

        private void DisableAnimator()
        {
            _thisAnimator.Play(Constants.Twinkle, 0, 0);
            _thisAnimator.speed = 0;
        }

        private void EnableAnimator()
        {
            _thisAnimator.speed = 1;
            _thisAnimator.Play(Constants.Twinkle, 0, 0);
        }
    }
}
