using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

namespace Showroom.Customization {
    [RequireComponent(typeof(Renderer))]
    [ExecuteInEditMode]
    public class CustomizationMaterialControl : MonoBehaviour {
        
        private static readonly int PROPERTY_OUTLINE_ENABLED = Shader.PropertyToID("_OutlineEnabled");
        private static readonly int PROPERTY_CURRENT_ALBEDO = Shader.PropertyToID("_CurrentAlbedo"); 
        private static readonly int PROPERTY_TARGET_ALBEDO = Shader.PropertyToID("_TargetAlbedo");
        private static readonly int PROPERTY_ALBEDO_BLEND = Shader.PropertyToID("_AlbedoBlend");

        private const float MATERIAL_BLEND_DURATION = 0.3f;
        
        [SerializeField] private bool _outlineEnabled;
        private Renderer _renderer;
        private MaterialPropertyBlock _propertyBlock;

        public bool OutlineEnabled { get { return _outlineEnabled; } set { _outlineEnabled = value; } }

        public void SetCustomizationOption(CustomizationOption customizationOption) {
            StopAllCoroutines();
            
            Material[] materials = _renderer.materials;

            Assert.IsTrue(materials.Length == customizationOption.MaterialOptions.Length, "Material customization options do not equal the number of materials!");

            for (int i = 0; i < materials.Length; i++) {
                materials[i].SetTexture(PROPERTY_TARGET_ALBEDO, customizationOption.GetTargetAbledo(i));
            }

            StartCoroutine(LerpMaterialAlbedos(materials, customizationOption));
        }

        private IEnumerator LerpMaterialAlbedos(Material[] materials, CustomizationOption customizationOption) {
            
            float time = 0.0f;
            
            do {

                float progress = time / MATERIAL_BLEND_DURATION;

                for (int i = 0; i < materials.Length; i++) {
                    materials[i].SetFloat(PROPERTY_ALBEDO_BLEND, progress);
                }

                time += Time.deltaTime;
                yield return 0;
            } while (time <= MATERIAL_BLEND_DURATION);

            for (int i = 0; i < materials.Length; i++) {
                materials[i].SetTexture(PROPERTY_CURRENT_ALBEDO, customizationOption.GetTargetAbledo(i));
                materials[i].SetFloat(PROPERTY_ALBEDO_BLEND, 0.0f);
            }
        }

        private void Start() {
            Initialize();
        }

        private void Reset() {
            Initialize();
        }

        private void OnValidate() {
            Initialize();
        }

        private void Initialize() {
            if(_renderer != null && _propertyBlock != null) return;

            _renderer = GetComponent<Renderer>();
            _propertyBlock = new MaterialPropertyBlock ();
        }

        private void Update() {
            _propertyBlock.SetFloat(PROPERTY_OUTLINE_ENABLED, _outlineEnabled ? 1.0f : 0.0f);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
