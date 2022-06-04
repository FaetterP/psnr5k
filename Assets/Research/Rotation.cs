using UnityEngine;

namespace Assets.Research
{
    class Rotation : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objects;
        [SerializeField] private float _radius = 50;
        [SerializeField] private float _rotationSpeed = 5;

        private void Start()
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                GameObject created = Instantiate(_objects[i], transform);
                Destroy(_objects[i].gameObject);

                float phi = 2 * Mathf.PI * i / _objects.Length;
                created.transform.localPosition = new Vector3(_radius * Mathf.Cos(phi), 0, _radius * Mathf.Sin(phi));
                _objects[i] = created;
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * _rotationSpeed);
        }
    }
}
