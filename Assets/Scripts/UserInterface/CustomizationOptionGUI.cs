using Showroom.Customization;
using Showroom.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Showroom.UserInterface {
	[RequireComponent(typeof(Animator))]
	public class CustomizationOptionGUI : MonoBehaviour {

		private static readonly int AnimHidden = Animator.StringToHash("Hidden");

		[SerializeField] private Text _text;
		[SerializeField] private Image _image;
		[SerializeField] private Button _button;
		[SerializeField] private Animator _animator;
		private CustomizationOption _customizationOption;

		public void Show(CustomizationOption customizationOption, float delay = 0.0f) {
			
			CustomCoroutine.WaitThenExecute(delay, () => {
				_button.onClick.RemoveAllListeners();
				_button.onClick.AddListener(OnClick);
            
				_customizationOption = customizationOption;

				_text.text = customizationOption.Name;
				_image.sprite = customizationOption.Preview;
				
				_animator.SetBool(AnimHidden, false);
			});
		}
        
		public void Hide() {
			_animator.SetBool(AnimHidden, true);
		}

		public void OnShowAnimationEnd() {
			_text.raycastTarget = _image.raycastTarget = _button.targetGraphic.raycastTarget = true;
		}

		public void OnHideAnimationEnd() {
			_text.raycastTarget = _image.raycastTarget = _button.targetGraphic.raycastTarget = false;
		}

		private void OnClick() {
			_customizationOption?.Select();
		}
	}
}