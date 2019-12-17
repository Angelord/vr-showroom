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

        public override void OnPreviewClick(InteractionEvent ev) {
            _preview.MoveTo(ev.Point);
        }
    }
}
