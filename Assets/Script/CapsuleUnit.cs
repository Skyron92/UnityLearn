using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleUnit : MonoBehaviour
{
    [SerializeField] private Material baseMaterial, SelectedMaterial;
    private MeshRenderer _MeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        _MeshRenderer=GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelected() {
        _MeshRenderer.material = SelectedMaterial;

   }
    public void OnSDeselected()
    {
        _MeshRenderer.material = baseMaterial;
    }
}
