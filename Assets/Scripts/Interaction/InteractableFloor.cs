using System;
using System.Collections;
using System.Collections.Generic;
using Showroom;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Showroom.Interaction {
    public class InteractableFloor : Interactable {

        [SerializeField] private Preview _preview;
        [SerializeField] private FloorPointer _pointer;

        public override void OnPreviewClick(InteractionEvent ev) {
            _preview.MoveTo(ev.Point);
        }

        public override void OnPreviewFocus(InteractionEvent ev) {
            _pointer.MoveTo(ev.Point);
        }

        public override void OnPreviewFocusGained(InteractionEvent ev) {
            _pointer.Enable(ev.Point);
        }

        public override void OnPreviewFocusLost(InteractionEvent ev) {
            _pointer.Disable();
        }
    }
}
