using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Showroom.Interaction {
    public class InteractableObject : Interactable {

        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Preview _preview;
        [SerializeField] private ObjectInformation _information;
        
        public Transform InitialTransform => _initialTransform;

        public ObjectInformation Information => _information;

        public override void OnPreviewClick(InteractionEvent ev) {
            _preview.FocusOn(this);
        }
    }
}