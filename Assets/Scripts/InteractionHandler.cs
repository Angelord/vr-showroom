using Showroom.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = System.Diagnostics.Debug;

namespace Showroom {
	public class InteractionHandler {

		private Camera _camera;
		private Interactable _focusedInteractable;
		private bool _clickInitiated = false;
		
		public InteractionHandler(Camera camera) {
			this._camera = camera;
		}

		public void PreventClick() {
			_clickInitiated = false;
		}

		public void OnPointerEnter(Interactable obj, PointerEventData eventData) {
			if (_focusedInteractable != null) {
				_focusedInteractable.OnPreviewFocusLost(new InteractionEvent());
			}

			_focusedInteractable = obj;
			_focusedInteractable.OnPreviewFocusGained(GenerateInteraction());
			_clickInitiated = false;
		}

		public void OnPointerExit(Interactable obj, PointerEventData eventData) {
			if (_focusedInteractable != null) {
				_focusedInteractable.OnPreviewFocusLost(new InteractionEvent());
			}

			_focusedInteractable = null;
			_clickInitiated = false;
		}

		public void OnPointerDown(Interactable obj, PointerEventData eventData) {
			_clickInitiated = true;
		}

		public void OnPointerUp(Interactable obj, PointerEventData eventData) {
			if (_clickInitiated) {
				_focusedInteractable.OnPreviewSelected(GenerateInteraction());
			}
		}

		public void FixedUpdate() {
			if (_focusedInteractable == null) { return; }
			
			InteractionEvent ev = GenerateInteraction();
			_focusedInteractable.OnPreviewFocus(ev);
		}
		
		private InteractionEvent GenerateInteraction() {
			InteractionEvent ev = new InteractionEvent();
            
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition - new Vector3(0.0f, 12.0f));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 200.0f)) {
				// TODO : verify we are hitting an interactable
				ev.Point = hit.point;
			}

			return ev;
		}
	}
}