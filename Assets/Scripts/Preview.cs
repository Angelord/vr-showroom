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
        [SerializeField] private CustomizationGUI _customizationGui;
        private PreviewNavigation _activeNavigation;
        private InteractionHandler _interactionHandler;
        private bool _waiting;

        public bool Locked {
            get { return _waiting || _activeNavigation.Locked; }
        }

        public InteractionHandler InteractionHandler { get => _interactionHandler; }

        private void Awake() {
            _interactionHandler = new InteractionHandler(GetComponentInChildren<Camera>());
        }

        private void Start() {
            _activeNavigation = _freeNavigation;
            _focusNavigation.enabled = false;
        }

        public void MoveTo(Vector3 position) {
            _waiting = true;
            _customizationGui.Hide();
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
            _customizationGui.Hide();
            CustomCoroutine.WaitThenExecute(0.3f, () => {
                _focusNavigation.enabled = true;
                _freeNavigation.enabled = false;
                _activeNavigation = _focusNavigation;
                _focusNavigation.FocusOn(target);
                _infoGui.Show(target.Information);
                _customizationGui.Show(target.CustomizationOptions);
                _waiting = false;
            });
        }
        
        private void Update() {
            if (Locked) {
                _interactionHandler.PreventClick();
            }
        }

        private void FixedUpdate() {
            if (!Locked) {
                _interactionHandler.FixedUpdate();
            }
        }
    }
}