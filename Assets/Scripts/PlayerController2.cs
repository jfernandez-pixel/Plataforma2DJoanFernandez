using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D _rBody;

    private InputAction _jumpAction;
    private InputAction _moveAction;
    private Vector2 _moveInput;

    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField]private float _jumpforce = 5f;
    [SerializeField]private Transform _groundSensor;
    [SerializeField]private float _sensorRadius = 0.5f;
    
    void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _jumpAction = InputSystem.actions["Jump"];
        _moveAction = InputSystem.actions["Move"];
    }
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();

        if(_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler (0, 0, 0);
        }
        else if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler (0, 180, 0);
        }

        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
    }
}

    void FixedUpdate()
    {
        _rBody.linearVelocityX = _moveInput.x * _movementSpeed;
    }

void Jump()
{
    _rBody.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
}
