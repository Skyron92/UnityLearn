using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeManager : MonoBehaviour
{
    private List<CubeUnit> _cubeUnits = new List<CubeUnit>();
    [SerializeField] private InputActionReference mouseInputActionReference;
    private InputAction MouseInputAction => mouseInputActionReference.action;
    [SerializeField, Range(0,100)] private float range;
    [SerializeField] private LayerMask selectableLayer;
    
    void Awake() {
        MouseInputAction.Enable();
        MouseInputAction.started += context => SelectCubeUnit();
    }

    void SelectCubeUnit() {
        Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayon, out RaycastHit hit, range, selectableLayer)) {
            CubeUnit unit = hit.transform.gameObject.GetComponent<CubeUnit>();
            _cubeUnits.Add(unit);
            unit.OnSelected();
        }
        else {
            foreach (var unit in _cubeUnits) {
                unit.OnDeselected();
            }
            _cubeUnits.Clear();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Camera.main.transform.position, range);
    }
}