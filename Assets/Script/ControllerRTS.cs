using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRTS : MonoBehaviour

{
    private List<CapsuleUnit> _CapsuleUnitList=new List<CapsuleUnit>();
    [SerializeField] private InputActionReference MouseInputActionReference;
    private InputAction MouseInputAction => MouseInputActionReference;
    [SerializeField, Range(0,100)] private float range;
    [SerializeField] private LayerMask SelectableLayer;
    // Start is called before the first frame update
    void Start()
    {
        MouseInputAction.started+=context=>SelectCapsuleUnit(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        MouseInputAction.Enable();
        MouseInputAction.started -= context=>SelectCapsuleUnit();
    }

    void SelectCapsuleUnit()
    {
       Ray rayon= Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayon, out RaycastHit hit,range, SelectableLayer)){
            CapsuleUnit unit  = hit.transform.gameObject.GetComponent<CapsuleUnit>();
            _CapsuleUnitList.Add(unit);
            unit.OnSelected();

        
        }
        else
        {
            foreach (var unit in _CapsuleUnitList)
            {
                unit.OnSDeselected();
            }
            _CapsuleUnitList.Clear();
        }

    

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Camera.main.transform.position, range);
    }


}
