using DG.Tweening;
using UnityEngine;

namespace Showroom.Navigation {
    public class FreeNavigation : PreviewNavigation {
        
        [SerializeField] private float _yOffset = 1.6f;
        [SerializeField] private float _rotateSensitivity = 2.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _inertiaMultiplier = 0.94f;
        private bool _rotating;
        private bool _moving;
        private Vector3 _rotInertia = Vector3.zero;

        public override bool Locked => _rotating || _moving;

        public void MoveTo(Vector3 position) {
                        
            Vector3 offsetPos = position;
            offsetPos.y = _yOffset;

            _moving = true;
            
            var tween = transform.DOMove(offsetPos, 1.0f);
            tween.onComplete += () => { _moving = false; };
        }

        private void Update() {
            
            _rotating = false;

            Vector3 input = Vector3.zero;
            if (Input.GetMouseButton(0)) {
                input = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0.0f);
            }
            
            Vector3 offset = input + _rotInertia;
            offset *= _rotateSensitivity;

            _rotInertia += input / 2;
            _rotInertia *= _inertiaMultiplier;
        
            transform.Rotate(offset);

            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z = 0.0f;
            transform.rotation = Quaternion.Euler(rotation);

            if (offset.sqrMagnitude > 0.1f) {
                _rotating = true;
            }
        }
    }
}