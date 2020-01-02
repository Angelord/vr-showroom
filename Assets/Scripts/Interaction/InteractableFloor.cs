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

        public override void OnPreviewMouse(InteractionEvent ev) {
            _pointer.MoveTo(ev.Point);
        }

        public override void OnPreviewMouseEnter(InteractionEvent ev) {
            _pointer.Enable(ev.Point);
        }

        public override void OnPreviewMouseExit(InteractionEvent ev) {
            _pointer.Disable();
        }

        protected override void OnPreviewSelected(InteractionEvent ev) {
            _pointer.Click();
            Preview.MoveTo(ev.Point);
        }
    }
}
