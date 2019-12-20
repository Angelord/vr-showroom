using UnityEngine;

namespace Showroom.Materials {
	[CreateAssetMenu(fileName = "FloatMaterialProperty", menuName = "MaterialProperties/Float", order = 1)]
	public class MaterialPropertyFloat : MaterialProperty {

		[SerializeField] private float _value;

		public float Value {
			get => _value;
			set => _value = value;
		}

		public override void Apply(MaterialPropertyBlock propertyBlock) {
			propertyBlock.SetFloat(Identifier, _value);
		}
	}
}