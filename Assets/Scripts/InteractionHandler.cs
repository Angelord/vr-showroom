using Showroom.Interaction;
using UnityEngine;

namespace Showroom {
	public class InteractionHandler {

		private Camera _camera;
		private Interactable _targetInteractable;
		private bool _clickCanceled;
		
		public InteractionHandler(Camera camera) {
			this._camera = camera;
		}

		public void PreventInteraction() {
			_clickCanceled = true;
		
//			_targetInteractable?.OnPreviewFocusLost(new InteractionEvent());
//			_targetInteractable = null;
		}

		public void Update() {
			if (Input.GetMouseButtonDown(0)) {
				_clickCanceled = false;
			}

			if (Input.GetMouseButtonUp(0) && !_clickCanceled) {
				InteractionEvent ev;
				_targetInteractable = RaycastInteractable(out ev);
				_targetInteractable?.OnPreviewClick(ev);
			}
		}

		public void FixedUpdate() {
			InteractionEvent ev;
			Interactable newTarget = RaycastInteractable(out ev);
            
			if (newTarget != _targetInteractable) {
				_targetInteractable?.OnPreviewFocusLost(ev);
				_targetInteractable = newTarget;
				_targetInteractable?.OnPreviewFocusGained(ev);
			}
			else {
				_targetInteractable?.OnPreviewFocus(ev);
			}
		}
		
		private Interactable RaycastInteractable(out InteractionEvent ev) {
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition - new Vector3(0.0f, 12.0f));
			RaycastHit hit;
			ev = new InteractionEvent();
            
			if (Physics.Raycast(ray, out hit, 200.0f)) {
				ev.Point = hit.point;
				return hit.collider.GetComponentInChildren<Interactable>();
			}
            
			return null;
		}
	}
}