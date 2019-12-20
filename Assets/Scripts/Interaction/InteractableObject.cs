using System;
using System.Collections.Generic;
using Showroom.Customization;
using Showroom.Materials;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Showroom.Interaction {
    public class InteractableObject : Interactable {

        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Transform _focusCenter;
        [SerializeField] private ObjectInformation _information;
        [SerializeField] private MaterialPropertyBool _outlineEnabledProp;
        [SerializeField] private List<CustomizationOption> _customizationOptions;
        [SerializeField] private int _defaultOption;
        private MeshRenderer _renderer;
        
        public Vector3 InitialPosition => _initialTransform.position;
        public Vector3 FocusCenter => _focusCenter.position;

        public ObjectInformation Information => _information;

        public IReadOnlyList<CustomizationOption> CustomizationOptions => _customizationOptions;

        private void Start() {
            _renderer = GetComponent<MeshRenderer>();
            foreach (CustomizationOption customizationOption in _customizationOptions) {
                customizationOption.OnSelect += OnSelectOption;
            }
            _customizationOptions[_defaultOption].Select();
        }

        private void OnSelectOption(CustomizationOption option) {
            _renderer.materials = option.Materials;
            _information.Material = option.Name;
        }

        public override void OnPreviewClick(InteractionEvent ev) {
            Preview.FocusOn(this);
        }

        public override void OnPreviewFocusGained(InteractionEvent ev) {
            _outlineEnabledProp.Value = true;
        }

        public override void OnPreviewFocusLost(InteractionEvent ev) {
            _outlineEnabledProp.Value = false;
        }
    }
}