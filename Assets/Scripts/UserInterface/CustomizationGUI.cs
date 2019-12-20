using System.Collections;
using System.Collections.Generic;
using Showroom.Customization;
using UnityEngine;

namespace Showroom.UserInterface {
    public class CustomizationGUI : MonoBehaviour {

        [SerializeField] private Transform _layoutGroup;
        [SerializeField] private GameObject _optionPrefab;
        private List<CustomizationOptionGUI> _optionGuis = new List<CustomizationOptionGUI>();
        
        public void Show(IReadOnlyList<CustomizationOption> customizationOptions) {

            int requiredOptionGuis = customizationOptions.Count - _optionGuis.Count;
            for (int i = 0; i < requiredOptionGuis; i++) {
                CreateOptionGui();
            }

            for (int i = 0; i < customizationOptions.Count; i++) {
                _optionGuis[i].Show(customizationOptions[i]);
            }
        }
        
        public void Hide() {
            foreach (CustomizationOptionGUI optionGui in _optionGuis) {
                optionGui.Hide();
            }
        }

        private void CreateOptionGui() {
            CustomizationOptionGUI newOptionGui =
                Instantiate(_optionPrefab, _layoutGroup).GetComponent<CustomizationOptionGUI>();

            _optionGuis.Add(newOptionGui);
        }
    }
}
