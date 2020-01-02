using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Showroom.Interaction {
	public abstract class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

		[SerializeField] private Preview _preview;
		private bool _selected;

		public bool Selected { get { return _selected; } }

		protected Preview Preview {
			get { return _preview; }
		}

		public void OnPointerEnter(PointerEventData eventData) {
			_preview.InteractionHandler.OnPointerEnter(this, eventData);
		}
		
		public void OnPointerExit(PointerEventData eventData) {
			_preview.InteractionHandler.OnPointerExit(this, eventData);
		}

		public void OnPointerDown(PointerEventData eventData) {
			_preview.InteractionHandler.OnPointerDown(this, eventData);
		}

		public void OnPointerUp(PointerEventData eventData) {
			_preview.InteractionHandler.OnPointerUp(this, eventData);
		}

		public void Select(InteractionEvent ev) {
			_selected = true;
			OnPreviewSelected(ev);
		}

		public void Deselect() {
			_selected = false;
			OnPreviewDeselected();
		}

		public virtual void OnPreviewFocusGained(InteractionEvent ev) {
		}

		public virtual void OnPreviewFocusLost(InteractionEvent ev) {
		}

		public virtual void OnPreviewFocus(InteractionEvent ev) {
		}

		protected virtual void OnPreviewSelected(InteractionEvent ev) {
		}

		protected virtual void OnPreviewDeselected() {
		}
	}
}