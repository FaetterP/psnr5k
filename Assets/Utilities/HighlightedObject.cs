using UnityEngine;

namespace Assets.Utilities
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(MeshRenderer))]
    class HighlightedObject : MonoBehaviour
    {
        [SerializeField] private Color _enableColor = Color.green;
        [SerializeField] private Color _disableColor = Color.white;

        private Collider _thisCollider;
        private MeshRenderer _thisMeshRenderer;
        private Camera _camera;

        private bool _isActive = false;
        public bool IsActive => _isActive;

        private void Awake()
        {
            _camera = Camera.main;
            _thisCollider = GetComponent<Collider>();
            _thisMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _thisMeshRenderer.material.color = _disableColor;
        }

        private void Update()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_camera.transform.position, ray.direction, out RaycastHit hit, 1000))
            {
                if (hit.collider == _thisCollider)
                    Enable();
                else
                    Disable();
            }
            else
                Disable();
        }

        private void Enable()
        {
            if (_isActive)
                return;

            _isActive = true;
            _thisMeshRenderer.material.color = _enableColor;
        }

        private void Disable()
        {
            if (_isActive == false)
                return;

            _isActive = false;
            _thisMeshRenderer.material.color = _disableColor;
        }
    }
}
