using System;
using UnityEngine;
using Showroom.Control;

namespace Showroom {

    public class Preview : MonoBehaviour {

        [SerializeField] private FreeControl _freeControl;
        [SerializeField] private FocusControl _focusControl;
        private PreviewControl _activeControl;

        public bool Locked {
            get { return _activeControl.Locked; }
        }

        private void Start() {
            _activeControl = _freeControl;
            _focusControl.enabled = false;
        }

        public void MoveTo(Vector3 position) {
            _focusControl.enabled = false;
            _freeControl.enabled = true;
            _activeControl = _freeControl;
            _freeControl.MoveTo(position);
        }

        public void FocusOn(FocusObject target) {
            _focusControl.enabled = true;
            _freeControl.enabled = false;
            _activeControl = _focusControl;
            _focusControl.FocusOn(target);
        }
    }
}