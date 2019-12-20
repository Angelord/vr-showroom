using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Showroom.Materials {
    [RequireComponent(typeof(Renderer))]
    [ExecuteInEditMode]
    public class PerRendererMaterialOptions : MonoBehaviour {

        [SerializeField] private List<MaterialProperty> _materialProperties = new List<MaterialProperty>();
        private MaterialPropertyBlock _propertyBlock ;
        private Renderer _renderer;

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
            
            foreach (var materialProperty in _materialProperties) {
                materialProperty?.Apply(_propertyBlock);
            }
            _renderer.SetPropertyBlock (_propertyBlock);
        }
    }
}
