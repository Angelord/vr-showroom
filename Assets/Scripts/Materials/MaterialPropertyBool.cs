using UnityEngine;

namespace Showroom.Materials {
	[CreateAssetMenu(fileName = "BoolMaterialProperty", menuName = "MaterialProperties/Bool", order = 2)]
	public class MaterialPropertyBool : MaterialProperty {

		[SerializeField] private bool _value;

		public bool Value {
			get => _value;
			set => _value = value;
		}

		public override void Apply(MaterialPropertyBlock propertyBlock) {
			propertyBlock.SetFloat(Identifier, _value ? 1.0f : 0.0f);
		}
	}
}