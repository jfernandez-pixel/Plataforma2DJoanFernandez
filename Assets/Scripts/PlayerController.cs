using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private int _maxHealth = 100;
    [SerializeField]private float _movementSpeed = 4.5f;
    [SerializeField]private float _JumpHeight = 2;
    
    private Rigidbody2D _rigidbody2D;

    private InputAction _jumpAction;
    private InputAction _moveAction; 
    private Vector2 _moveInput;

    [SerializeField]private Transform _groundSensor;
    [SerializeField]private float _sensorSize = 1;
    [SerializeField]private LayerMask _groundLayer;

    void Awake() //Hace lo mismo que el start pero se ejecuta antes
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>(); //Rotar el personaje cuando gira

        if(_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler (0, 0, 0);
        }



        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate() //Se ejecuta el mismo numero de veces x segundo
    {
                _rigidbody2D.linearVelocity = new Vector2(_moveInput.x * _movementSpeed, _rigidbody2D.linearVelocity.y);
    }

    void Jump() //Funcion de salto donde se llama al rigidbody pq es qn tiene las fisicas y se le anyade una fuerza up hacia donde la envia
    {
        _rigidbody2D.AddForce(Vector2.up * Mathf.Sqrt(_JumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse); //
    }

    bool IsGrounded() // El bool es para que la funcion devuelva algo (verdadero o falso)
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_groundSensor.position, _sensorSize);

        foreach(Collider2D item in colliders2D)
        {
            if(item.gameObject.layer == 6)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundSensor.position, _sensorSize);
    }
}
