using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 1.0f;

    private float _startMovementSpeed;

    public bool _isPaused = false;

    bool _isGrounded = false;
    protected const float GROUND_CHECK_DISTANCE = 0.2f;
    protected const string GROUND_LAYER = "Ground";

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    protected Vector3 _desiredLookatPoint = Vector3.zero;
    protected GameObject _target;


    protected Rigidbody _rigidBody;
    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }

    public Vector3 DesiredLookatPoint
    {
        get { return _desiredLookatPoint; }
        set { _desiredLookatPoint = value; }
    }
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }


    protected virtual void Start()
    {
        _startMovementSpeed = _movementSpeed;
    }
    protected virtual void FixedUpdate()
    {
        if (!_isPaused)
        {
            HandleMovement();
            RotateWithInput();
        }
        else
        {
            _rigidBody.velocity = Vector3.zero; 
        }
        //check ground collision
        _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down,
            GROUND_CHECK_DISTANCE, LayerMask.GetMask("Ground"));
    }

    protected virtual void HandleMovement()
    {
        Debug.Assert(_rigidBody != null, "Rigidbody does not exist");
        if (_rigidBody == null) return;

        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed;

        //maintain vertical velocity as it was otherwise gravity would be stripped out
       // movement.y = _rigidBody.velocity.y;
        _rigidBody.velocity = movement;



    }
    public void SetMovementSpeed(int movementSpeed)
    {
        _movementSpeed = movementSpeed;
        Debug.Log( "Movement speed changed - " + _movementSpeed);
    }

    public void AddMovementSpeed(float movementSpeedIncrease)
    {
        _movementSpeed += movementSpeedIncrease;

    }
    public void ResetMovementSpeed()
    {
        _movementSpeed = _startMovementSpeed;
    }

    protected void RotateWithInput()
    {
        if (_desiredMovementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(DesiredMovementDirection, Vector3.up);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f); //no limit to how much rotation is allowed;

            _rigidBody.MoveRotation(rotation);
        }

    }

}
