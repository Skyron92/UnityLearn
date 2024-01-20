using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CubeUnit : MonoBehaviour {
    
    [SerializeField] private Material baseMaterial, selectedMaterial;
    private MeshRenderer _meshRenderer;
    protected NavMeshAgent NavMeshAgent;
    public CubeState currentState;
    [SerializeField, Range(0f, 60f)] protected float fieldOfView;
    public static List<CubeUnit> EnemiesList = new List<CubeUnit>();
    protected Vector3 Destination;
    
    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        EnemiesList.Add(this);
    }
    private void Update() {
        switch (currentState) {
            case CubeState.Normal :
                StartCoroutine(CheckEnemiesInRange());
                break;
            case CubeState.Run :
                StartCoroutine(SetDestination());
                break;
            case CubeState.Fight :
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected abstract IEnumerator CheckEnemiesInRange(); 
    protected abstract IEnumerator SetDestination(); 

    protected abstract void Run();
    
    public void OnSelected() {
        _meshRenderer.material = selectedMaterial;
    }

    public void OnDeselected() {
        _meshRenderer.material = baseMaterial;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfView);
    }
}

public enum CubeState {
    Normal,
    Fight,
    Run
}