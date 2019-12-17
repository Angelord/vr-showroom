using DG.Tweening;
using UnityEngine;
using Showroom.Interaction;

namespace Showroom.Navigation {
    public class FocusNavigation : PreviewNavigation {

        [SerializeField] private float _rotateSensitivity = 2.0f;
        private InteractableObject _target;
        private Transform _focusPoint;
        private bool _focusing;
        private bool _rotating;
        
        public override bool Locked => _rotating;

        private void Awake() {
            _focusPoint = (new GameObject()).GetComponent<Transform>();
        }

        private void OnDisable() {
            transform.SetParent(null);
            _target = null;
        }

        public void FocusOn(InteractableObject target) {
            if (_target == target) {
                return;
            }

            _target = target;
            
            transform.SetParent(null);
            _focusPoint.position = _target.transform.position;
            transform.SetParent(_focusPoint);
            _focusing = true;
            
            var tween = transform.DOMove(_target.InitialTransform.position, 1.5f);
            tween.onUpdate += () => {
                Vector3 lookVector = _target.transform.position - transform.position;
                Quaternion targetRot = Quaternion.LookRotation(lookVector, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.5f);
            };

            tween.onComplete += () => {
                transform.LookAt(target.transform.position);
                _focusing = false;
            };
        }

        private void Update() {

            if(!_focusing && Input.GetMouseButton(0)) {
                Vector3 offset = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
                offset *= _rotateSensitivity;

                _focusPoint.Rotate(Vector3.up, offset.x, Space.World);

                Vector3 lookDir = _focusPoint.position - transform.position;
                Vector3 camRight = Vector3.Cross(lookDir, Vector3.up);
                _focusPoint.Rotate(camRight, offset.y, Space.World);

                if (offset.sqrMagnitude > 0.1f) {
                    _rotating = true;
                }
            }
            else {
                _rotating = false;
            }
        }
    }
}