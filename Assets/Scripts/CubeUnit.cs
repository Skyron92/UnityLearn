using System;
using UnityEngine;

public class CubeUnit : MonoBehaviour {
    
    [SerializeField] private Material baseMaterial, selectedMaterial;
    private MeshRenderer _meshRenderer;

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnSelected() {
        _meshRenderer.material = selectedMaterial;
    }

    public void OnDeselected() {
        _meshRenderer.material = baseMaterial;
    }
    
}