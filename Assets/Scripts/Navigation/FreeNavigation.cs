using DG.Tweening;
using UnityEngine;

namespace Showroom.Navigation {
    public class FreeNavigation : PreviewNavigation {
        
        [SerializeField] private float _yOffset = 1.6f;
        [SerializeField] private float _rotateSensitivity = 2.0f;
        private bool _rotating;
        private bool _moving;

        public override bool Locked => _rotating || _moving;

        public void MoveTo(Vector3 position) {
                        
            Vector3 offsetPos = position;
            offsetPos.y = _yOffset;

            _moving = true;
            
            var tween = transform.DOMove(offsetPos, 1.0f);
            tween.onComplete += () => { _moving = false; };
        }

        private void Update() {

            if(Input.GetMouseButton(0)) {
                Vector3 offset = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0.0f);
                offset *= _rotateSensitivity;
            
                transform.Rotate(offset);

                Vector3 rotation = transform.rotation.eulerAngles;
                rotation.z = 0.0f;
                transform.rotation = Quaternion.Euler(rotation);
            
                _rotating = offset.sqrMagnitude > 0.1f;
            }
            else {
                _rotating = false;
            }
        }
    }
}