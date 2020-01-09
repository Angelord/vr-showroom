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
		[SerializeField] [HideInInspector] private Sprite _previewSprite;
		
		public event Action<CustomizationOption> OnSelect;

		public string Name => _name;
		
		public Texture2D Preview => _preview;

		public Sprite PreviewSprite => _previewSprite;

		public MaterialOptions[] MaterialOptions => _materialOptions;
		
		private void OnValidate() {
			_previewSprite = Sprite.Create(_preview, new Rect(0.0f, 0.0f, _preview.width, _preview.height), new Vector2(0.5f, 0.5f));
		}

		public Texture2D GetTargetAbledo(int index) {
			return _materialOptions[index]._abledo;
		}

		public void Select() {
			OnSelect?.Invoke(this);
		}
	}
}
