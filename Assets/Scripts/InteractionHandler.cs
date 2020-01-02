using Showroom.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = System.Diagnostics.Debug;

namespace Showroom {
	public class InteractionHandler {

		private readonly Camera _camera;
		private Interactable _selectedInteractable;
		private Interactable _hoveredInteractable;
		private bool _clickInitiated = false;
		
		public InteractionHandler(Camera camera) {
			_camera = camera;
		}

		public void PreventClick() {
			_clickInitiated = false;
		}

		public void OnPointerEnter(Interactable obj, PointerEventData eventData) {
			if (_hoveredInteractable != null) {
				_hoveredInteractable.OnPreviewMouseExit(new InteractionEvent());
			}

			_hoveredInteractable = obj;
			_hoveredInteractable.OnPreviewMouseEnter(GenerateInteraction());
			_clickInitiated = false;
		}

		public void OnPointerExit(Interactable obj, PointerEventData eventData) {
			if (_hoveredInteractable != null) {
				_hoveredInteractable.OnPreviewMouseExit(new InteractionEvent());
			}

			_hoveredInteractable = null;
			_clickInitiated = false;
		}

		public void OnPointerDown(Interactable obj, PointerEventData eventData) {
			_clickInitiated = true;
		}

		public void OnPointerUp(Interactable obj, PointerEventData eventData) {
			if (_clickInitiated) {
				_selectedInteractable?.Deselect();

				_selectedInteractable = _hoveredInteractable;
				_hoveredInteractable.Select(GenerateInteraction());
			}
		}

		public void FixedUpdate() {
			if (_hoveredInteractable == null) { return; }
			
			InteractionEvent ev = GenerateInteraction();
			_hoveredInteractable.OnPreviewMouse(ev);
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