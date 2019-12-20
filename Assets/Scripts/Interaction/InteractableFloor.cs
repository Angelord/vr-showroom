using System;
using System.Collections;
using System.Collections.Generic;
using Showroom;
using Showroom.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Showroom.Interaction {
    public class InteractableFloor : Interactable {

        [SerializeField] private FloorPointer _pointer;

        public override void OnPreviewClick(InteractionEvent ev) {
            _pointer.Click();
            Preview.MoveTo(ev.Point);
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
