using System.Collections.Generic;
using Showroom.Customization;
using Showroom.Materials;
using UnityEngine;
using UnityEngine.Assertions;

namespace Showroom.Interaction {
    [RequireComponent(typeof(CustomizationMaterialControl))]
    public class InteractableObject : Interactable {

        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Transform _focusCenter;
        [SerializeField] private ObjectInformation _information;
        [SerializeField] private List<CustomizationOption> _customizationOptions;
        [SerializeField] private int _defaultOption;
        private CustomizationMaterialControl _materialControl;
        
        public Vector3 InitialPosition => _initialTransform.position;
        public Vector3 FocusCenter => _focusCenter.position;

        public ObjectInformation Information => _information;

        public IReadOnlyList<CustomizationOption> CustomizationOptions => _customizationOptions;

        private void Start() {
            Assert.AreNotEqual(0, _customizationOptions.Count, $"No customization options provided for {name}");

            _materialControl = GetComponent<CustomizationMaterialControl>();
            
            foreach (CustomizationOption customizationOption in _customizationOptions) {
                customizationOption.OnSelect += OnSelectOption;
            }
            
            OnSelectOption(_customizationOptions[_defaultOption]);
        }

        private void OnSelectOption(CustomizationOption option) {
            _materialControl.SetCustomizationOption(option);
            _information.Material = option.Name;
        }

        public override void OnPreviewMouseEnter(InteractionEvent ev) {
            if (!Selected) {
                _materialControl.OutlineEnabled = true;
            }
        }

        public override void OnPreviewMouseExit(InteractionEvent ev) {
            _materialControl.OutlineEnabled = false;
        }

        protected override void OnPreviewSelected(InteractionEvent ev) {
            _materialControl.OutlineEnabled = false;
            Preview.FocusOn(this);
        }
    }
}