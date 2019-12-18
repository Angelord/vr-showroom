using System.Collections;
using System.Collections.Generic;
using Showroom.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Showroom.UserInterface {
	[RequireComponent(typeof(Animator))]
	public class ObjectInformationGUI : MonoBehaviour {

		private static readonly int AnimPropHidden = Animator.StringToHash("Hidden");
		
		[SerializeField] private Text _nameText;
		[SerializeField] private Text _dimensionsText;
		[SerializeField] private Text _materialText;
		[SerializeField] private Image _separatorLine;

		private Animator _animator;
		private ObjectInformation _current;

		private void Start() {
			_animator = GetComponent<Animator>();
			Hide();
		}

		public void Show(ObjectInformation information) {
			if (_current != null) {
				_current.OnMaterialChange -= OnMaterialChange;
				_animator.SetBool(AnimPropHidden, true);
			}

			_current = information;
			_current.OnMaterialChange += OnMaterialChange;

			CustomCoroutine.WaitOneFrameThenExecute(() => {
				_animator.SetBool(AnimPropHidden, false);
			});
		}

		public void Hide() {
			if (_current != null) {
				_current.OnMaterialChange -= OnMaterialChange;
				_current = null;
			}
			
			_animator.SetBool(AnimPropHidden, true);
		}

		public void OnShowAnimationStarted() {
			_nameText.text = _current.Name;
			_dimensionsText.text = _current.Dimensions;
			_materialText.text = _current.Material;
		}

		private void OnMaterialChange() {
			
			_materialText.text = _current.Material;
			
			//TODO : Play Mat transition animation
		}
	}
}
