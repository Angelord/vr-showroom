using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Showroom.UserInterface {
	public class ObjectInformationGUI : MonoBehaviour {

		[SerializeField] private Text _nameText;
		[SerializeField] private Text _dimensionsText;
		[SerializeField] private Text _materialText;
		[SerializeField] private Image _separatorLine;
		
		private ObjectInformation _current;

		private void Start() {
			Hide();
		}

		public void Show(ObjectInformation information) {
			if (_current != null) {
				_current.OnMaterialChange -= OnMaterialChange;
			}

			_current = information;
			_current.OnMaterialChange += OnMaterialChange;

			_nameText.text = _current.Name;
			_dimensionsText.text = _current.Dimensions;
			_materialText.text = _current.Material;
			_separatorLine.gameObject.SetActive(true);
			//TODO : Play show animation
		}

		public void Hide() {
			if (_current != null) {
				_current.OnMaterialChange -= OnMaterialChange;
				_current = null;
			}

			_nameText.text = "";
			_dimensionsText.text = "";
			_materialText.text = "";
			_separatorLine.gameObject.SetActive(false);
			//TODO : Play hide animation
		}

		private void OnMaterialChange() {
			
			_materialText.text = _current.Material;
			
			//TODO : Play Mat transition animation
		}
	}
}
