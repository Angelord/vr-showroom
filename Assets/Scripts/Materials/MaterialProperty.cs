using UnityEngine;

namespace Showroom.Materials {
	public abstract class MaterialProperty : ScriptableObject {

		[SerializeField] private string _identifier;

		public string Identifier => _identifier;

		public abstract void Apply(MaterialPropertyBlock propertyBlock);
	}
}