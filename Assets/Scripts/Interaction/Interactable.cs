using UnityEngine;


namespace Showroom.Interaction {
	public abstract class Interactable : MonoBehaviour {
		
		public virtual void OnPreviewFocusGained(InteractionEvent ev) {
		}
		
		public virtual void OnPreviewFocusLost(InteractionEvent ev) {
		}

		public virtual void OnPreviewFocus(InteractionEvent ev) {
		}
		
		public virtual void OnPreviewClick(InteractionEvent ev) {
		}
	}
}