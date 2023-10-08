 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//based on Dan Pos Unity Interaction system

public class InteractorBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _interactPoint;
    [SerializeField]
    private float _interactPointRadius;
    [SerializeField]
    private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[4];

    [SerializeField] private int _numFound;


    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactPoint.position, _interactPointRadius, _colliders, 
            _interactableMask); //will fill the array of colliders with objects found around the interactPoint, that also have the same tag as _interactableMask

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactPoint.position, _interactPointRadius);
    }


}
