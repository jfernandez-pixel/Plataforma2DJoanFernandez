/*using UnityEngine;
using UnityEngine.InputSystem;


public class Repaso : MonoBehaviour
{
    private Rigidbody2D _rBody;


    private InputAction _jumpAction;
    private InputAction _moveAction;
    private Vector2 _moveInput;
    private Animator _animator; //Animacion


    [SerializeField]private float _movementSpeed = 10f;
    [SerializeField]private float _jumpforce = 5f;
    [SerializeField]private Transform _groundSensor;
    [SerializeField]private float _sesnorRadius = 0.5f;


    void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();


        _jumpAction = InputSystem.actions["Jump"];
        _moveAction = InputSystem.actions["Move"];
        _animator = GetComponent<Animator>();//Animacion
    }
   
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();


        if(_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRunning", true);//Animacion
        }
        else if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRunning", true);//Animacion
        }
        else()//Animacion
        {
            _animator.SetBool("IsRunning", false);//Animacion
        }


        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
        _animator.SetBool("IsJumping", !IsGrounded());//Animacion
    }


    void FixedUpdate()
    {
        _rBody.linearVelocityX = _moveInput.x * _movementSpeed;
    }


    void Jump()
    {
        _rBody.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
    }


    bool IsGrounded()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_groundSensor.position, _sesnorRadius);


        foreach (Collider2D item in colliders2D)
        {
            if(item.gameObject.layer == 6)
            {
                return true;
            }
        }


        return false;
    }
}*/