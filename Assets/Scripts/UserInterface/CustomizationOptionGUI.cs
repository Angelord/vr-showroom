using Showroom.Customization;
using UnityEngine;
using UnityEngine.UI;

namespace Showroom.UserInterface {
	public class CustomizationOptionGUI : MonoBehaviour {

		[SerializeField] private Text _text;
		[SerializeField] private Image _image;
		[SerializeField] private Button _button;
		private CustomizationOption _customizationOption;

		public void Show(CustomizationOption customizationOption) {

			_button.onClick.RemoveAllListeners();
			_button.onClick.AddListener(OnClick);
            
			_customizationOption = customizationOption;

			_text.text = customizationOption.Name;
			_image.sprite = customizationOption.Preview;
            
			this.gameObject.SetActive(true);
		}
        
		public void Hide() {
			this.gameObject.SetActive(false);
		}

		private void OnClick() {
			_customizationOption?.Select();
		}
	}
}