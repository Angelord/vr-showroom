using System;
using DG.Tweening;
using UnityEditor.Media;
using UnityEngine;

namespace Showroom.Control {
    public class FocusControl : PreviewControl {

        [SerializeField] private float _rotateSensitivity = 2.0f;
        private FocusObject _target;
        private Transform _focusPoint;
        private bool _rotating;
        
        public override bool Locked => _rotating;

        private void Start() {
            _focusPoint = new GameObject().GetComponent<Transform>();
        }

        private void OnDisable() {
            transform.SetParent(null);
            _target = null;
        }

        public void FocusOn(FocusObject target) {
            if (_target == target) {
                return;
            }

            _target = target;
            
            _focusPoint.position = _target.transform.position;
            transform.SetParent(_focusPoint);
            
            var tween = transform.DOMove(_target.InitialTransform.position, 1.0f);
            tween.onUpdate += () => {
                Vector3 lookVector = _target.transform.position - transform.position;
//                Vector3 up = Vector3.Cross(lookVector, transform.right);
                Quaternion targetRot = Quaternion.LookRotation(lookVector, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.5f);
            };

            tween.onComplete += () => {
                transform.LookAt(target.transform.position);
            };
        }

        private void Update() {
//            Vector3 rotation = transform.rotation.eulerAngles;
//            rotation.z = 0.0f;
//            transform.rotation = Quaternion.Euler(rotation);

            if(Input.GetMouseButton(0)) {
                Vector3 offset = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
                offset *= _rotateSensitivity;

                _focusPoint.Rotate(Vector3.up, offset.x);

                Vector3 lookDir = _focusPoint.position - transform.position;
                Vector3 camRight = Vector3.Cross(lookDir, Vector3.up);
;                _focusPoint.Rotate(camRight, offset.y);

                _rotating = offset.sqrMagnitude > 0.1f;
            }
        }
    }
}