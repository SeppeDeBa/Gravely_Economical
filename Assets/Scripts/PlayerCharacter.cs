using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField]
    private InputActionAsset _inputAsset;

    [SerializeField]
    private InputActionReference _horizontalMovementAction;

    [SerializeField]
    private InputActionReference _verticalMovementAction;

    [SerializeField]
    private CorpseInventory _playerCorpseInventory;

    private InputAction _interactAction; 


    protected override void Awake()
    {
        base.Awake();

        if (_inputAsset == null) return;

        //Example of searching for the binding in code, alternatively, they can be hooked in the editor using a InputActionReference as shown by _movementActuib
        _interactAction = _inputAsset.FindActionMap("Gameplay").FindAction("Interact");


        //we bind a callback to it instead of continuously monitoring input
        _interactAction.performed += HandleInteract;
    }

    
    public void increaseMovementSpeed(float msIncrease)
    {
        _movementBehaviour.AddMovementSpeed(msIncrease);
    }

    private void OnEnable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Enable();
    }
    private void OnDisable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Disable();
    }
    private void Update()
    {
        HandleMovementInput();
        //HandleInteract();
        HandleAimInput();
    }

    void HandleMovementInput()
    {
        if (_movementBehaviour == null ||
            _horizontalMovementAction == null ||
            _verticalMovementAction == null) return;

        //movement
        float horizontalMovementInput = _horizontalMovementAction.action.ReadValue<float>();
        float verticalMovementInput = _verticalMovementAction.action.ReadValue<float>();
        Vector3 movement;
        movement.x = horizontalMovementInput;
        movement.y = 0;
        movement.z = verticalMovementInput;
        
        
        _movementBehaviour.DesiredMovementDirection = movement;
        
    }

    private void HandleInteract(InputAction.CallbackContext context)
    {
        //seperated to an interactor system to decouple
        //Debug.Assert(false, "pressed interact key"); //test to see if button works
    }

    void HandleAimInput()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);

        Vector3 worldMousePostion = Camera.main.ScreenToWorldPoint(mousePosition);
        //worldMousePostion.z = 0;
        _movementBehaviour.DesiredLookatPoint = worldMousePostion;
    }



}


