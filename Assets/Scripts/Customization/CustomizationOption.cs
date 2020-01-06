using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Showroom.Customization {
	[CreateAssetMenu(fileName = "Customization Option", menuName = "Customization/Option", order = 1)]
	public class CustomizationOption : ScriptableObject {

		[SerializeField] private string _name;
		[SerializeField] private Texture2D _preview;
		[SerializeField] private Material[] _materials;

		public event Action<CustomizationOption> OnSelect;

		public string Name => _name;

		public Texture2D Preview => _preview;

		public Material[] Materials => _materials;

		public void Select() {
			OnSelect?.Invoke(this);
		}

	}
}
