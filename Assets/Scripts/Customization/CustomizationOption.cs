using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Showroom.Customization {
	[System.Serializable]
	public class MaterialOptions {
		public Texture2D _abledo;
	}

	[CreateAssetMenu(fileName = "Customization Option", menuName = "Customization/Option", order = 1)]
	public class CustomizationOption : ScriptableObject {

		[SerializeField] private string _name;
		[SerializeField] private Texture2D _preview;
		[SerializeField] private MaterialOptions[] _materialOptions;

		public event Action<CustomizationOption> OnSelect;

		public string Name => _name;

		public Texture2D Preview => _preview;

		public MaterialOptions[] MaterialOptions => _materialOptions;

		public Texture2D GetTargetAbledo(int index) {
			return _materialOptions[index]._abledo;
		}

		public void Select() {
			OnSelect?.Invoke(this);
		}

	}
}
