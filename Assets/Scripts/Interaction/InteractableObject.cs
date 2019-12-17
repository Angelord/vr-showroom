using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Showroom.Interaction {
    public class InteractableObject : Interactable {

        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Preview _preview;
        
        public Transform InitialTransform => _initialTransform;
        
        public override void OnPreviewClick(InteractionEvent ev) {
            _preview.FocusOn(this);
        }
    }
}