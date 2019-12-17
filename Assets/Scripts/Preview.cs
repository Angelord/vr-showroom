using System;
using DG.Tweening.Plugins;
using UnityEngine;
using Showroom.Navigation;
using Showroom.Interaction;
using UnityEngine.Serialization;

namespace Showroom {

    public class Preview : MonoBehaviour {

        [FormerlySerializedAs("_freeControl")] [SerializeField] private FreeNavigation _freeNavigation;
        [FormerlySerializedAs("_focusControl")] [SerializeField] private FocusNavigation _focusNavigation;
        private PreviewNavigation _activeNavigation;
        private InteractionHandler _interactionHandler;

        public bool Locked {
            get { return _activeNavigation.Locked; }
        }

        private void Start() {
            _interactionHandler = new InteractionHandler(GetComponent<Camera>());
            _activeNavigation = _freeNavigation;
            _focusNavigation.enabled = false;
        }

        public void MoveTo(Vector3 position) {
            _focusNavigation.enabled = false;
            _freeNavigation.enabled = true;
            _activeNavigation = _freeNavigation;
            _freeNavigation.MoveTo(position);
        }

        public void FocusOn(InteractableObject target) {
            _focusNavigation.enabled = true;
            _freeNavigation.enabled = false;
            _activeNavigation = _focusNavigation;
            _focusNavigation.FocusOn(target);
        }
        
        private void Update() {
            if (Locked) {
                _interactionHandler.PreventClick();
            }

            _interactionHandler.Update();
        }

        private void FixedUpdate() {
            _interactionHandler.FixedUpdate();
        }
    }
}