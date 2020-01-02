using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Showroom.Interaction {
	public abstract class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

		[SerializeField] private Preview _preview;
		
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

		public virtual void OnPreviewFocusGained(InteractionEvent ev) {
		}

		public virtual void OnPreviewFocusLost(InteractionEvent ev) {
		}

		public virtual void OnPreviewFocus(InteractionEvent ev) {
		}

		public virtual void OnPreviewSelected(InteractionEvent ev) {
		}
	}
}