using System;
using System.Collections;
using System.Collections.Generic;
using Showroom;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ClickableFloor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField] private Preview _preview;

    private bool _focused;
    
    public void OnPointerDown(PointerEventData eventData) {
        _focused = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        if(_focused) {
            _preview.MoveTo(eventData.pointerCurrentRaycast.worldPosition);
            _focused = false;
        }
    }
    
    private void Update() {
        if (_preview.Locked) {
            _focused = false;
        }
    }
}