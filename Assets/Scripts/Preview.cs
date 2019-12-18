using System;
using DG.Tweening.Plugins;
using UnityEngine;
using Showroom.Navigation;
using Showroom.Interaction;
using Showroom.UserInterface;
using Showroom.Utility;
using UnityEngine.Serialization;

namespace Showroom {

    public class Preview : MonoBehaviour {

        [SerializeField] private FreeNavigation _freeNavigation;
        [SerializeField] private FocusNavigation _focusNavigation;
        [SerializeField] private ObjectInformationGUI _infoGui;
        private PreviewNavigation _activeNavigation;
        private InteractionHandler _interactionHandler;
        private bool _waiting;

        public bool Locked {
            get { return _waiting || _activeNavigation.Locked; }
        }

        private void Start() {
            _interactionHandler = new InteractionHandler(GetComponent<Camera>());
            _activeNavigation = _freeNavigation;
            _focusNavigation.enabled = false;
        }

        public void MoveTo(Vector3 position) {
            _waiting = true;
            CustomCoroutine.WaitThenExecute(0.3f, () => {
                _focusNavigation.enabled = false;
                _freeNavigation.enabled = true;
                _activeNavigation = _freeNavigation;
                _freeNavigation.MoveTo(position);
                _infoGui.Hide();
                _waiting = false;
            });
        }

        public void FocusOn(InteractableObject target) {
            _waiting = true;
            CustomCoroutine.WaitThenExecute(0.3f, () => {
                _focusNavigation.enabled = true;
                _freeNavigation.enabled = false;
                _activeNavigation = _focusNavigation;
                _focusNavigation.FocusOn(target);
                _infoGui.Show(target.Information);
                _waiting = false;
            });
        }
        
        private void Update() {
            if (!Locked) {
                _interactionHandler.Update();
            }
            else {
                _interactionHandler.PreventInteraction();
            }
        }

        private void FixedUpdate() {
            if (!Locked) {
                _interactionHandler.FixedUpdate();
            }
        }
    }
}