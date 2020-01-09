using DG.Tweening;
using UnityEngine;
using Showroom.Interaction;
using UnityEngine.Serialization;

namespace Showroom.Navigation {
    public class FocusNavigation : PreviewNavigation {

        private const float MinRotationZ = 76.0f;
        private const float MaxRotationZ = 156.0f;

        [SerializeField] private float _rotateSensitivity = 2.0f;
        [SerializeField] private float _maxZoom = 1.5f;
        [SerializeField] private float _zoomSpeed = 2.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _inertiaMultiplier = 0.9f;
        private Camera _camera;
        private InteractableObject _target;
        private Transform _focusPoint;
        private bool _focusing;
        private bool _rotating;
        private float _zoomIntertia = 0.0f;
        private Vector3 _rotInertia = Vector2.zero;
        
        public override bool Locked => _rotating || _focusing;

        private void Awake() {
            _focusPoint = (new GameObject()).GetComponent<Transform>();
        }

        private void Start() {
            _camera = GetComponentInChildren<Camera>();
        }

        public override void OnExit() {
            transform.SetParent(null);
            _target = null;

            if (_camera != null) {
                // Restore default zoom
                _camera.transform.DOLocalMove(Vector3.zero, 0.5f);
            }
        }

        public void FocusOn(InteractableObject target) {
            if (_target == target) {
                return;
            }

            _target = target;
            
            transform.SetParent(null);
            _focusPoint.position = _target.FocusCenter;
            transform.SetParent(_focusPoint);
            _focusing = true;
            
            var tween = transform.DOMove(_target.InitialPosition, 1.5f);
            
            tween.onUpdate += () => {
                Vector3 lookVector = _target.FocusCenter - transform.position;
                Quaternion targetRot = Quaternion.LookRotation(lookVector, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.5f);
            };

            tween.onComplete += () => {
                transform.LookAt(target.FocusCenter);
                _focusing = false;
            };
        }

        private void Update() {
            if (_focusing) {
                _rotating = false;
                return;
            }

            HandleZoom();
            
            HandleRotation();
        }

        private void HandleZoom() {
            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            mouseWheel = Mathf.Clamp(_zoomIntertia + mouseWheel, -1.0f, 1.0f);
            
            float cameraZ = _camera.transform.localPosition.z;
            if ((mouseWheel > 0.0001f && cameraZ < _maxZoom) || (mouseWheel < -0.0001f && cameraZ > 0.0f)) {
                _camera.transform.Translate(Time.deltaTime * _zoomSpeed * mouseWheel * Vector3.forward);
            }
            
            _zoomIntertia *= _inertiaMultiplier;
            _zoomIntertia += mouseWheel;
            _zoomIntertia /= 2;
        }

        private void HandleRotation() {
            _rotating = false;

            Vector3 input = Vector3.zero;
            if (Input.GetMouseButton(0)) {
                input = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            }

            Vector3 offset = input + _rotInertia;
            offset *= _rotateSensitivity;

            _rotInertia += input / 2;
            _rotInertia *= _inertiaMultiplier;
            
            _focusPoint.Rotate(Vector3.up, offset.x, Space.World);

            float camAngle = Vector3.Angle(_camera.transform.forward, Vector3.up);
            if ((offset.y > 0.0f && camAngle > MinRotationZ) || (offset.y < 0.0f && camAngle < MaxRotationZ)) {
                Vector3 lookDir = _focusPoint.position - transform.position;
                Vector3 camRight = Vector3.Cross(lookDir, Vector3.up);
                _focusPoint.Rotate(camRight, offset.y, Space.World);
            }

            if (offset.sqrMagnitude > 0.1f) {
                _rotating = true;
            }
        }
    }
}