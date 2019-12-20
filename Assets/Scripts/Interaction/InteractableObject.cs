using System;
using Showroom.Materials;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Showroom.Interaction {
    public class InteractableObject : Interactable {

        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Transform _focusCenter;
        [SerializeField] private Preview _preview;
        [SerializeField] private ObjectInformation _information;
        [SerializeField] private MaterialPropertyBool _outlineEnabledProp;
        
        public Vector3 InitialPosition => _initialTransform.position;
        public Vector3 FocusCenter => _focusCenter.position;

        public ObjectInformation Information => _information;

        public override void OnPreviewClick(InteractionEvent ev) {
            _preview.FocusOn(this);
        }

        public override void OnPreviewFocusGained(InteractionEvent ev) {
            _outlineEnabledProp.Value = true;
        }

        public override void OnPreviewFocusLost(InteractionEvent ev) {
            _outlineEnabledProp.Value = false;
        }
    }
}