using Assets.Scripts.UI.Research;
using UnityEngine;

namespace Assets.Scripts.Research
{
    class Rotation : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        [SerializeField] private float _radius = 50;
        [SerializeField] private float _rotationSpeed = 5;
        [SerializeField] private KeyCode _stopKey;
        [SerializeField] private ObjectGroup _group;
        private bool _isRotate;

        private void Awake()
        {
            _isRotate = true;
        }

        private void Start()
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                GameObject created = Instantiate(_objects[i], transform);
                Destroy(_objects[i].gameObject);

                float phi = 2 * Mathf.PI * i / _objects.Length;
                created.transform.localPosition = new Vector3(_radius * Mathf.Cos(phi), 0, _radius * Mathf.Sin(phi));
                _objects[i] = created;

                if (_group != null)
                {
                    _group.AddElement(created.GetComponent<ObjectWithMessage>());
                }

            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_stopKey))
            {
                _isRotate = !_isRotate;
            }

            if (_isRotate)
            {
                transform.Rotate(Vector3.up * _rotationSpeed);
            }
        }
    }
}
