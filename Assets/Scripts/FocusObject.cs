﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Showroom {
    
    public class FocusObject : MonoBehaviour, IPointerClickHandler {

        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Preview _preview;
        
        public Transform InitialTransform => _initialTransform;
        
        public void OnPointerClick(PointerEventData eventData) {
            _preview.FocusOn(this);
        }
    }
}