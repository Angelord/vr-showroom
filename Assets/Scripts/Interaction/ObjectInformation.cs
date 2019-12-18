using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInformation {

	[SerializeField] private string _name;
	[SerializeField] private string _dimensions;
	[SerializeField] private string _material;

	public event Action OnMaterialChange;

	public string Name => _name;

	public string Dimensions => _dimensions;
	
	public string Material {
		get { return _material; }
		set {
			_material = value;
			OnMaterialChange?.Invoke();
		}
	}
}
