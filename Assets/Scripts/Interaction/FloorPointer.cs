using UnityEngine;

namespace Showroom.Interaction {
	
	public class FloorPointer : MonoBehaviour {

		public void MoveTo(Vector3 pos) {
			Vector3 newPos = pos;
			newPos.y = transform.position.y;
			transform.position = newPos;
		}

		public void Enable(Vector3 initialPos) {
			MoveTo(initialPos);
			this.gameObject.SetActive(true);
		}

		public void Disable() {
			this.gameObject.SetActive(false);
		}
	}
}